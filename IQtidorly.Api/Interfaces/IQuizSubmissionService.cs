using IQtidorly.Api.ViewModels.QuizSubmissions;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IQuizSubmissionService
    {
        Task<bool> SubmitAnswerAsync(SubmitAnswerModel viewModel);
    }
}
