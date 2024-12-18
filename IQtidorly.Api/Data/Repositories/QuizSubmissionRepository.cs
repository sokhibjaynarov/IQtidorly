using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.QuizSubmissions;

namespace IQtidorly.Api.Data.Repositories
{
    public class QuizSubmissionRepository : BaseRepository<QuizSubmission, ApplicationDbContext>, IQuizSubmissionRepository
    {
        public QuizSubmissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
