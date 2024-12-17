using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.OlympiadResults;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IOlympiadResultRepository : IBaseRepository<OlympiadResult, ApplicationDbContext>
    {
    }
}
