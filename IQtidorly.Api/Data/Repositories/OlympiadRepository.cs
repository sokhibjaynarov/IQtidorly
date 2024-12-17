using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.Olympiads;

namespace IQtidorly.Api.Data.Repositories
{
    public class OlympiadRepository : BaseRepository<Olympiad, ApplicationDbContext>, IOlympiadRepository
    {
        public OlympiadRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
