using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.Subjects;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface ISubjectRepository : IBaseRepository<Subject, ApplicationDbContext>
    {
    }
}
