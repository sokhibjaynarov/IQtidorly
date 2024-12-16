using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using IQtidorly.Api.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<JsonResponse> SignUp(CreateUserViewModel createUserView)
        {
            try
            {
                return JsonResponse.DataResponse(await _identityService.CreateUserAsync(createUserView));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<JsonResponse> Login(CreateTokenViewModel createTokenView)
        {
            try
            {
                return JsonResponse.DataResponse(await _identityService.CreateTokenAsync(createTokenView));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("recover-password")]
        [AllowAnonymous]
        public async Task<JsonResponse> CreateRecoverPassword(string emailAddress)
        {
            try
            {
                return JsonResponse.DataResponse(await _identityService.CreatePasswordRecoveryEmailAsync(emailAddress));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpGet("verify-email")]
        [AllowAnonymous]
        public async Task<JsonResponse> VerifyEmail(string email, string verifactionCode)
        {
            try
            {
                return JsonResponse.DataResponse(await _identityService.VerifyEmailAsync(email, verifactionCode));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("delete-account")]
        public async Task<JsonResponse> CreateUserRemovalEmail(string password)
        {
            try
            {
                return JsonResponse.DataResponse(await _identityService.CreateUserRemovalEmailAsync(password));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("update-password")]
        public async Task<JsonResponse> UpdatePassword(UpdatePasswordViewModel updatePasswordView)
        {
            try
            {
                return JsonResponse.DataResponse(await _identityService.UpdatePasswordAsync(updatePasswordView));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("verify-recover-password")]
        [AllowAnonymous]
        public async Task<JsonResponse> RecoverPassword(RecoverPasswordViewModel recoverPasswordView)
        {
            try
            {
                return JsonResponse.DataResponse(await _identityService.RecoverPasswordAsync(recoverPasswordView));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpGet("verify-delete-account")]
        [AllowAnonymous]
        public async Task<JsonResponse> VerifyUserRemovalEmail(string email, string verifactionCode)
        {
            try
            {
                return JsonResponse.DataResponse(await _identityService.VerifyUserRemovalAsync(email, verifactionCode));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }
    }
}
