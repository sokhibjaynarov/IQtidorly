using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.QuizQuestions;
using IQtidorly.Api.Models.Quizzes;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.Quizzes;
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
    public class QuizService : BaseService, IQuizService
    {
        public QuizService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<QuizService> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task<Guid> CreateQuizAsync(QuizForCreateModel viewModel)
        {
            try
            {
                if (string.IsNullOrEmpty(viewModel.Title) || !viewModel.QuizQuestions.Any())
                {
                    throw new Exception("Name and questions must be provided");
                }

                var quiz = _mapper.Map<Quiz>(viewModel);

                quiz = await _unitOfWorkRepository.QuizRepository.AddAsync(quiz);

                foreach (var question in viewModel.QuizQuestions)
                {
                    var quizQuestion = new QuizQuestion
                    {
                        QuizId = quiz.QuizId,
                        QuestionId = question.QuestionId
                    };

                    await _unitOfWorkRepository.QuizQuestionRepository.AddAsync(quizQuestion);
                }

                if (await _unitOfWorkRepository.QuizRepository.SaveChangesAsync() > 0)
                {
                    return quiz.QuizId;
                }

                throw new Exception("Failed to save quiz");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<QuizForGetModel> GetQuizByIdAsync(Guid quizId)
        {
            try
            {
                var quiz = await _unitOfWorkRepository.QuizRepository.GetAsync(quizId);

                if (quiz == null)
                {
                    throw new Exception("Quiz not found");
                }

                return _mapper.Map<QuizForGetModel>(quiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<(List<QuizForGetModel> Quizzes, int Count)> GetAllQuizzesAsPaginationAsync(int take, int skip)
        {
            try
            {
                var quizzesQuery = _unitOfWorkRepository.QuizRepository.GetAll();

                var quizzes = await quizzesQuery.Skip(skip).Take(take).ToListAsync();
                var count = await quizzesQuery.CountAsync();

                if (quizzes == null)
                {
                    return (new List<QuizForGetModel>(), count);
                }

                var quizzesForGetModel = _mapper.Map<List<QuizForGetModel>>(quizzes);

                return (quizzesForGetModel, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteQuizAsync(Guid quizId)
        {
            try
            {
                var quiz = await _unitOfWorkRepository.QuizRepository.GetAsync(quizId);

                if (quiz == null)
                {
                    throw new Exception("Quiz not found");
                }

                if (quiz.StartDate < DateTime.Now)
                {
                    throw new Exception("Quiz cannot be deleted because it has started");
                }

                var quizQuestions = await _unitOfWorkRepository.QuizQuestionRepository.GetAll().Where(p => p.QuizId == quizId).ToListAsync();

                _unitOfWorkRepository.QuizQuestionRepository.RemoveRange(quizQuestions);

                _unitOfWorkRepository.QuizRepository.Remove(quiz);

                if (await _unitOfWorkRepository.QuizRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("Failed to delete quiz");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateQuizAsync(QuizForUpdateModel viewModel)
        {
            try
            {
                var quiz = await _unitOfWorkRepository.QuizRepository.GetAsync(viewModel.QuizId);

                if (quiz == null)
                {
                    throw new Exception("Quiz not found");
                }

                if (quiz.StartDate < DateTime.Now)
                {
                    throw new Exception("Quiz cannot be updated because it has started");
                }

                quiz = _mapper.Map(viewModel, quiz);

                foreach (var question in viewModel.QuizQuestions)
                {
                    if (question.State == Enums.State.Create)
                    {
                        var quizQuestion = new QuizQuestion
                        {
                            QuizId = quiz.QuizId,
                            QuestionId = question.QuestionId
                        };

                        await _unitOfWorkRepository.QuizQuestionRepository.AddAsync(quizQuestion);
                    }
                    else if (question.State == Enums.State.Delete)
                    {
                        var quizQuestion = await _unitOfWorkRepository.QuizQuestionRepository.GetAll().FirstOrDefaultAsync(p => p.QuizId == quiz.QuizId && p.QuestionId == question.QuestionId);

                        if (quizQuestion != null)
                        {
                            _unitOfWorkRepository.QuizQuestionRepository.Remove(quizQuestion);
                        }
                    }
                }

                _unitOfWorkRepository.QuizRepository.Update(quiz);

                if (await _unitOfWorkRepository.QuizRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("Failed to update quiz");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
