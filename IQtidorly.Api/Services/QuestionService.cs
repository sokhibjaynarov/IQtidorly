using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.QuestionOptions;
using IQtidorly.Api.Models.Questions;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.Questions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IQtidorly.Api.Services
{
    public class QuestionService : BaseService, IQuestionService
    {
        public QuestionService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<QuestionService> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task<Guid> CreateQuestionAsync(QuestionForCreateModel questionForCreateModel)
        {
            try
            {
                var subjectChapter = await _unitOfWorkRepository.SubjectChapterRepository.GetAsync(questionForCreateModel.SubjectChapterId);

                if (subjectChapter == null)
                {
                    throw new Exception("Subject chapter not found");
                }

                var ageGroup = await _unitOfWorkRepository.AgeGroupRepository.GetAsync(questionForCreateModel.AgeGroupId);

                if (ageGroup == null)
                {
                    throw new Exception("Age group not found");
                }

                var question = _mapper.Map<Question>(questionForCreateModel);

                var options = _mapper.Map<List<QuestionOption>>(questionForCreateModel.Options.Where(p => p.State == Enums.State.Create));

                question = await _unitOfWorkRepository.QuestionRepository.AddAsync(question);

                foreach (var option in options)
                {
                    option.QuestionId = question.QuestionId;
                    await _unitOfWorkRepository.QuestionOptionRepository.AddAsync(option);
                }

                if (await _unitOfWorkRepository.QuestionRepository.SaveChangesAsync() > 0)
                {
                    return question.QuestionId;
                }

                throw new Exception("An error occurred while creating the question");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<(List<QuestionForGetModel> Questions, int Count)> GetAllQuestionsAsPaginationAsync(int take, int skip)
        {
            try
            {
                var questionsQuery = _unitOfWorkRepository.QuestionRepository.GetAll();

                var questions = await questionsQuery.Skip(skip).Take(take).ToListAsync();
                var count = await questionsQuery.CountAsync();

                if (questions == null)
                {
                    return (new List<QuestionForGetModel>(), count);
                }

                var questionsForGetModel = _mapper.Map<List<QuestionForGetModel>>(questions);

                return (questionsForGetModel, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
