using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.OlympiadQuestions;

namespace IQtidorly.Api.Data.Repositories
{
    public class OlympiadQuestionRepository : BaseRepository<OlympiadQuestion, ApplicationDbContext>, IOlympiadQuestionRepository
    {
        public OlympiadQuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
