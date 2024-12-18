using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
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

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
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
    }
}
