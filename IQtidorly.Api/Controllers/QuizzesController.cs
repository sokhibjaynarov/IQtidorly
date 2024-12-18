using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using IQtidorly.Api.ViewModels.QuizSubmissions;
using IQtidorly.Api.ViewModels.Quizzes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/quizzes")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly IQuizSubmissionService _quizSubmissionService;

        public QuizzesController(
            IQuizService quizService,
            IQuizSubmissionService quizSubmissionService)
        {
            _quizService = quizService;
            _quizSubmissionService = quizSubmissionService;
        }

        [HttpGet("getall")]
        public async Task<JsonResponse> GetAllQuizzesAsPaginationAsync([FromQuery] int take, [FromQuery] int skip)
        {
            try
            {
                var (quizzes, count) = await _quizService.GetAllQuizzesAsPaginationAsync(take, skip);

                var response = new PaginationResponse(quizzes, skip, take, count);

                return JsonResponse.DataResponse(response);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<JsonResponse> CreateQuizAsync([FromBody] QuizForCreateModel quizForCreateModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _quizService.CreateQuizAsync(quizForCreateModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }

        [HttpGet("getbyid")]
        public async Task<JsonResponse> GetQuizByIdAsync([FromQuery] Guid quizId)
        {
            try
            {
                return JsonResponse.DataResponse(await _quizService.GetQuizByIdAsync(quizId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<JsonResponse> UpdateQuizAsync([FromBody] QuizForUpdateModel quizForUpdateModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _quizService.UpdateQuizAsync(quizForUpdateModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<JsonResponse> DeleteQuizAsync([FromQuery] Guid quizId)
        {
            try
            {
                return JsonResponse.DataResponse(await _quizService.DeleteQuizAsync(quizId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }

        [HttpPost("start")]
        public async Task<JsonResponse> StartQuizAsync([FromQuery] Guid quizId)
        {
            try
            {
                return JsonResponse.DataResponse(await _quizService.StartQuizAsync(quizId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }

        [HttpPost("finish")]
        public async Task<JsonResponse> FinishQuizAsync([FromQuery] Guid quizId)
        {
            try
            {
                return JsonResponse.DataResponse(await _quizService.FinishQuizAsync(quizId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }

        [HttpPost("submit")]
        public async Task<JsonResponse> SubmitQuizAsync([FromBody] SubmitAnswerModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _quizSubmissionService.SubmitAnswerAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<JsonResponse> RegisterToQuizAsync([FromQuery] Guid quizId)
        {
            try
            {
                return JsonResponse.DataResponse(await _quizService.RegisterToQuizAsync(quizId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }
    }
}
