using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.Quizzes;

namespace IQtidorly.Api.Data.Repositories
{
    public class QuizRepository : BaseRepository<Quiz, ApplicationDbContext>, IQuizRepository
    {
        public QuizRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
