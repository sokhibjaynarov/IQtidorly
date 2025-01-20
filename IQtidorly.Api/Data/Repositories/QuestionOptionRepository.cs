using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Models.QuestionOptions;

namespace IQtidorly.Api.Data.Repositories
{
    public class QuestionOptionRepository : BaseRepository<QuestionOption, ApplicationDbContext>, IQuestionOptionRepository
    {
        public QuestionOptionRepository(ApplicationDbContext dbContext, IRequestLanguageHelper languageHelper) : base(dbContext, languageHelper)
        {
        }
    }
}
