using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.QuizSubmissions;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.QuizSubmissions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IQtidorly.Api.Services
{
    public class QuizSubmissionService : BaseService, IQuizSubmissionService
    {
        public QuizSubmissionService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<QuizSubmissionService> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task<bool> SubmitAnswerAsync(SubmitAnswerModel viewModel)
        {
            try
            {
                var userId = WebHelper.GetUserId(_httpContextAccessor.HttpContext);

                var quizParticipant = await _unitOfWorkRepository.QuizParticipantRepository.GetAll().FirstOrDefaultAsync(p => p.UserId == userId);

                if (quizParticipant == null)
                {
                    throw new System.Exception("User is not a participant of any quiz");
                }

                var quizSubmission = await _unitOfWorkRepository.QuizSubmissionRepository.GetAll()
                    .FirstOrDefaultAsync(p => p.QuizParticipantId == quizParticipant.QuizParticipantId && p.QuestionId == viewModel.QuestionId);

                if (quizSubmission != null)
                {
                    quizSubmission.Answer = viewModel.Answer;
                    quizSubmission.SelectedOptionId = viewModel.SelectedOptionId;

                    _unitOfWorkRepository.QuizSubmissionRepository.Update(quizSubmission);
                }
                else
                {
                    var submission = new QuizSubmission
                    {
                        QuizParticipantId = quizParticipant.QuizParticipantId,
                        QuestionId = viewModel.QuestionId,
                        Answer = viewModel.Answer,
                        SelectedOptionId = viewModel.SelectedOptionId
                    };

                    await _unitOfWorkRepository.QuizSubmissionRepository.AddAsync(submission);
                }

                if (await _unitOfWorkRepository.QuizSubmissionRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new System.Exception("Failed to save answer");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
