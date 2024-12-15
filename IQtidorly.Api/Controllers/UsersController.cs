using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IQtidorly.Api.Models.Users;

namespace IQtidorly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
    }
}
