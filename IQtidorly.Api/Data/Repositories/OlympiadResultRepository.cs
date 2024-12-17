using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.OlympiadResults;

namespace IQtidorly.Api.Data.Repositories
{
    public class OlympiadResultRepository : BaseRepository<OlympiadResult, ApplicationDbContext>, IOlympiadResultRepository
    {
        public OlympiadResultRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
