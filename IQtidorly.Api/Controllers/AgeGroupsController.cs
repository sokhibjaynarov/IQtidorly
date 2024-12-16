using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using IQtidorly.Api.ViewModels.AgeGroups;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/age-groups")]
    [ApiController]
    public class AgeGroupsController : ControllerBase
    {

        public AgeGroupsController(IAgeGroupService ageGroupService)
        {
            _ageGroupService = ageGroupService;
        }

        private readonly IAgeGroupService _ageGroupService;

        /// <summary>
        /// Create Age Group
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<JsonResponse> CreateAgeGroup(AgeGroupForCreateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _ageGroupService.CreateAgeGroupAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("update")]
        public async Task<JsonResponse> UpdateAgeGroup(AgeGroupForUpdateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _ageGroupService.UpdateAgeGroupAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpGet("getall")]
        public async Task<JsonResponse> GetAllAgeGroupAsPagination(int take, int skip)
        {
            try
            {
                var data = await _ageGroupService.GetAllAgeGroupsAsPaginationAsync(take, skip);
                var response = new PaginationResponse(data.AgeGroups, skip, take, data.Count);
                return JsonResponse.DataResponse(response);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("getbyid")]
        public async Task<JsonResponse> GetAgeGroupById(Guid ageGroupId)
        {
            try
            {
                return JsonResponse.DataResponse(await _ageGroupService.GetAgeGroupByIdAsync(ageGroupId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("delete")]
        public async Task<JsonResponse> DeleteAgeGroup(Guid ageGroupId)
        {
            try
            {
                return JsonResponse.DataResponse(await _ageGroupService.DeleteAgeGroupAsync(ageGroupId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }
    }
}
