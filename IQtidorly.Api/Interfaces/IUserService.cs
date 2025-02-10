using IQtidorly.Api.ViewModels.Users;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IUserService
    {
        Task<UserInfoViewModel> GetUserInfoAsync();
    }
}
