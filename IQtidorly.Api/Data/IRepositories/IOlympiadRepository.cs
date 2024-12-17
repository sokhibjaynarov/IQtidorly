using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.Olympiads;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IOlympiadRepository : IBaseRepository<Olympiad, ApplicationDbContext>
    {
    }
}
