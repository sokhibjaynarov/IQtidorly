using System.ComponentModel.DataAnnotations;

namespace IQtidorly.Api.ViewModels.Users
{
    public class RecoverPasswordViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string VerificationCode { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
