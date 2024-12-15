using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.SubjectChapters;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface ISubjectChapterRepository : IBaseRepository<SubjectChapter, ApplicationDbContext>
    {
    }
}
