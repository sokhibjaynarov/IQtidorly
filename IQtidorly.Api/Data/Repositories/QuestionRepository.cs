using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Models.Questions;

namespace IQtidorly.Api.Data.Repositories
{
    public class QuestionRepository : BaseRepository<Question, ApplicationDbContext>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext dbContext, IRequestLanguageHelper languageHelper) : base(dbContext, languageHelper)
        {
        }
    }
}
