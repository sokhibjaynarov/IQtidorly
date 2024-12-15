using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.Files;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IFileRepository : IBaseRepository<File, ApplicationDbContext>
    {
    }
}
