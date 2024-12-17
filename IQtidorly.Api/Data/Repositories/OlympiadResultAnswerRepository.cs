using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.OlympiadResultAnswers;

namespace IQtidorly.Api.Data.Repositories
{
    public class OlympiadResultAnswerRepository : BaseRepository<OlympiadResultAnswer, ApplicationDbContext>, IOlympiadResultAnswerRepository
    {
        public OlympiadResultAnswerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
