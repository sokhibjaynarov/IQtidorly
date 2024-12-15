using IQtidorly.Api.ViewModels.Users;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IIdentityService
    {
        Task<GeneratedTokenViewModel> CreateTokenAsync(CreateTokenViewModel createTokenView);
        Task<Guid> CreateUserAsync(CreateUserViewModel createUserView);
        Task<bool> CreateUserRemovalEmailAsync(string password);
        Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel updatePasswordView);
        Task<bool> VerifyEmailAsync(string email, string verificationCode);
        Task<bool> VerifyUserRemovalAsync(string email, string verificationCode);
        Task<bool> CreatePasswordRecoveryEmailAsync(string emailAddress);
        Task<bool> RecoverPasswordAsync(RecoverPasswordViewModel recoverPasswordView);
    }
}
