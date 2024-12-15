using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.AgeGroups;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IAgeGroupRepository : IBaseRepository<AgeGroup, ApplicationDbContext>
    {
    }
}
