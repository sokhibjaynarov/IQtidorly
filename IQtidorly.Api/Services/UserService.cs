using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.Users;
using IQtidorly.Api.Response;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace IQtidorly.Api.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<UserService> logger,
            UserManager<User> userManager) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
            _userManager = userManager;
        }

        public async Task<UserInfoViewModel> GetUserInfoAsync()
        {
            try
            {
                var userId = WebHelper.GetUserId(_httpContextAccessor.HttpContext);

                var user = await _unitOfWorkRepository.UserRepository.GetAsync(userId);

                if (user == null)
                {
                    throw new ErrorCodeException("User not found");
                }

                var userInfoViewModel = _mapper.Map<UserInfoViewModel>(user);

                var roles = await _userManager.GetRolesAsync(user);

                if (roles != null && roles.Any())
                {
                    userInfoViewModel.Roles.AddRange(roles);
                }

                return userInfoViewModel;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
