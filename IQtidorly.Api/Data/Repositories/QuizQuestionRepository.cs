using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.QuizQuestions;

namespace IQtidorly.Api.Data.Repositories
{
    public class QuizQuestionRepository : BaseRepository<QuizQuestion, ApplicationDbContext>, IQuizQuestionRepository
    {
        public QuizQuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
