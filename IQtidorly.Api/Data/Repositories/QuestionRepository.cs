using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.Questions;

namespace IQtidorly.Api.Data.Repositories
{
    public class QuestionRepository : BaseRepository<Question, ApplicationDbContext>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
