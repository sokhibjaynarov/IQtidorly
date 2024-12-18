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

        public async Task<Guid> CreateQuestionAsync(QuestionForCreateModel viewModel)
        {
            try
            {
                if (string.IsNullOrEmpty(viewModel.Content) || !viewModel.Options.Any(p => p.State == Enums.State.Create) ||
                    !viewModel.Options.Any(p => p.IsCorrect))
                {
                    throw new Exception("Content, options and correct option must be provided");
                }

                var subjectChapter = await _unitOfWorkRepository.SubjectChapterRepository.GetAsync(viewModel.SubjectChapterId);

                if (subjectChapter == null)
                {
                    throw new Exception("Subject chapter not found");
                }

                var ageGroup = await _unitOfWorkRepository.AgeGroupRepository.GetAsync(viewModel.AgeGroupId);

                if (ageGroup == null)
                {
                    throw new Exception("Age group not found");
                }

                var question = _mapper.Map<Question>(viewModel);

                var options = _mapper.Map<List<QuestionOption>>(viewModel.Options.Where(p => p.State == Enums.State.Create));

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

        public async Task<(List<QuestionForGetListModel> Questions, int Count)> GetAllQuestionsAsPaginationAsync(int take, int skip)
        {
            try
            {
                var questionsQuery = _unitOfWorkRepository.QuestionRepository.GetAll()
                    .Include(p => p.AgeGroup).Include(p => p.SubjectChapter).ThenInclude(p => p.Subject);

                var questions = await questionsQuery.Skip(skip).Take(take).ToListAsync();
                var count = await questionsQuery.CountAsync();

                if (questions == null)
                {
                    return (new List<QuestionForGetListModel>(), count);
                }

                var questionsForGetModel = _mapper.Map<List<QuestionForGetListModel>>(questions);

                return (questionsForGetModel, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<QuestionForGetModel> GetQuestionByIdAsync(Guid questionId)
        {
            try
            {
                var question = await _unitOfWorkRepository.QuestionRepository.GetAll()
                    .Include(p => p.AgeGroup).Include(p => p.SubjectChapter).ThenInclude(p => p.Subject)
                    .Include(p => p.Options).FirstOrDefaultAsync(p => p.QuestionId == questionId);

                if (question == null)
                {
                    throw new Exception("Question not found");
                }

                var options = await _unitOfWorkRepository.QuestionOptionRepository.GetAll().Where(p => p.QuestionId == questionId).ToListAsync();

                return _mapper.Map<QuestionForGetModel>(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteQuestionAsync(Guid questionId)
        {
            try
            {
                var question = await _unitOfWorkRepository.QuestionRepository.GetAsync(questionId);

                if (question == null)
                {
                    throw new Exception("Question does not exist!");
                }

                var quizzes = await _unitOfWorkRepository.QuizQuestionRepository.GetAll().Where(p => p.QuestionId == questionId).ToListAsync();

                if (quizzes.Any())
                {
                    throw new Exception("Question cannot be deleted because it is used in a quiz");
                }

                var options = await _unitOfWorkRepository.QuestionOptionRepository.GetAll().Where(p => p.QuestionId == questionId).ToListAsync();


                _unitOfWorkRepository.QuestionOptionRepository.RemoveRange(options);

                _unitOfWorkRepository.QuestionRepository.Remove(question);

                if (await _unitOfWorkRepository.QuestionRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("An error occurred while deleting the question");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateQuestionAsync(QuestionForUpdateModel viewModel)
        {
            try
            {
                var question = await _unitOfWorkRepository.QuestionRepository.GetAsync(viewModel.QuestionId);

                if (question == null)
                {
                    throw new Exception("Question not found");
                }

                if (string.IsNullOrEmpty(viewModel.Content) || !viewModel.Options.Any(p => p.IsCorrect &&
                    (p.State == Enums.State.Create || p.State == Enums.State.Created || p.State == Enums.State.Update)))
                {
                    throw new Exception("Content, options and correct option must be provided");
                }

                var subjectChapter = await _unitOfWorkRepository.SubjectChapterRepository.GetAsync(viewModel.SubjectChapterId);

                if (subjectChapter == null)
                {
                    throw new Exception("Subject chapter not found");
                }

                var ageGroup = await _unitOfWorkRepository.AgeGroupRepository.GetAsync(viewModel.AgeGroupId);

                if (ageGroup == null)
                {
                    throw new Exception("Age group not found");
                }

                question = _mapper.Map(viewModel, question);

                foreach (var option in viewModel.Options)
                {
                    if (option.State == Enums.State.Create)
                    {
                        var questionOption = _mapper.Map<QuestionOption>(option);
                        questionOption.QuestionId = question.QuestionId;
                        await _unitOfWorkRepository.QuestionOptionRepository.AddAsync(questionOption);
                    }
                    else if (option.State == Enums.State.Update)
                    {
                        var questionOption = await _unitOfWorkRepository.QuestionOptionRepository.GetAsync(option.QuestionOptionId.Value);
                        questionOption = _mapper.Map(option, questionOption);
                        _unitOfWorkRepository.QuestionOptionRepository.Update(questionOption);
                    }
                    else if (option.State == Enums.State.Delete)
                    {
                        var questionOption = await _unitOfWorkRepository.QuestionOptionRepository.GetAsync(option.QuestionOptionId.Value);
                        _unitOfWorkRepository.QuestionOptionRepository.Remove(questionOption);
                    }
                }

                _unitOfWorkRepository.QuestionRepository.Update(question);

                if (await _unitOfWorkRepository.QuestionRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("An error occurred while updating the question");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
