using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using IQtidorly.Api.ViewModels.Questions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<JsonResponse> GetAllQuestionsAsPaginationAsync([FromQuery] int take, [FromQuery] int skip)
        {
            try
            {
                var (questions, count) = await _questionService.GetAllQuestionsAsPaginationAsync(take, skip);

                var response = new PaginationResponse(questions, skip, take, count);

                return JsonResponse.DataResponse(response);
            }
            catch (System.Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResponse> CreateQuestionAsync([FromBody] QuestionForCreateModel questionForCreateModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _questionService.CreateQuestionAsync(questionForCreateModel));
            }
            catch (System.Exception ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }
    }
}
