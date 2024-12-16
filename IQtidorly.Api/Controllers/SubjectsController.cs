using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using IQtidorly.Api.ViewModels.Subjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("getall")]
        public async Task<JsonResponse> GetAllSubjectsAsPagination(int take, int skip)
        {
            try
            {
                var data = await _subjectService.GetSubjectsAsPaginationAsync(take, skip);
                var response = new PaginationResponse(data.Subjects, skip, take, data.Count);
                return JsonResponse.DataResponse(response);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpGet("getbyid")]
        public async Task<JsonResponse> GetSubjectById(Guid subjectId)
        {
            try
            {
                var data = await _subjectService.GetSubjectByIdAsync(subjectId);
                return JsonResponse.DataResponse(data);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("create")]
        public async Task<JsonResponse> CreateSubject(SubjectForCreateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _subjectService.CreateSubjectAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("update")]
        public async Task<JsonResponse> UpdateSubject(SubjectForUpdateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _subjectService.UpdateSubjectAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("delete")]
        public async Task<JsonResponse> DeleteSubject(Guid subjectId)
        {
            try
            {
                return JsonResponse.DataResponse(await _subjectService.DeleteSubjectAsync(subjectId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }
    }
}
