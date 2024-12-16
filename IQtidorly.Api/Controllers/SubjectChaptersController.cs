using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using IQtidorly.Api.ViewModels.SubjectChapters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/subject-chapters")]
    [ApiController]
    public class SubjectChaptersController : ControllerBase
    {
        private readonly ISubjectChapterService _subjectChapterService;

        public SubjectChaptersController(ISubjectChapterService subjectChapterService)
        {
            _subjectChapterService = subjectChapterService;
        }

        [HttpGet("getall")]
        public async Task<JsonResponse> GetAllSubjectChaptersAsPagination(int take, int skip)
        {
            try
            {
                var data = await _subjectChapterService.GetSubjectChaptersAsPaginationAsync(take, skip);
                var response = new PaginationResponse(data.SubjectChapters, skip, take, data.Count);
                return JsonResponse.DataResponse(response);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpGet("getbyid")]
        public async Task<JsonResponse> GetSubjectChapterById(Guid subjectChapterId)
        {
            try
            {
                var data = await _subjectChapterService.GetSubjectChapterByIdAsync(subjectChapterId);
                return JsonResponse.DataResponse(data);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("create")]
        public async Task<JsonResponse> CreateSubjectChapter(SubjectChapterForCreateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _subjectChapterService.CreateSubjectChapterAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("update")]
        public async Task<JsonResponse> UpdateSubjectChapter(SubjectChapterForUpdateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _subjectChapterService.UpdateSubjectChapterAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("delete")]
        public async Task<JsonResponse> DeleteSubjectChapter(Guid subjectChapterId)
        {
            try
            {
                return JsonResponse.DataResponse(await _subjectChapterService.DeleteSubjectChapterAsync(subjectChapterId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }
    }
}
