using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.QuizSubmissions;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IQuizSubmissionRepository : IBaseRepository<QuizSubmission, ApplicationDbContext>
    {
    }
}
