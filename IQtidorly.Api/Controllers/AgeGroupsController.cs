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
    }
}
