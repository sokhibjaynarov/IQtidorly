using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("userinfo")]
        public async Task<JsonResponse> GetUserInfo()
        {
            try
            {
                var userInfo = await _userService.GetUserInfoAsync();
                return JsonResponse.DataResponse(userInfo);
            }
            catch (ErrorCodeException ex)
            {
                return JsonResponse.ErrorResponse(ex.Message);
            }
        }
    }
}
