using IQtidorly.Api.ViewModels.Emails;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailViewModel viewModel);
    }
}
