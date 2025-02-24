using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.SubjectChapters;

namespace IQtidorly.Api.Data.Repositories
{
    public class SubjectChapterRepository : BaseRepository<SubjectChapter, ApplicationDbContext>, ISubjectChapterRepository
    {
        public SubjectChapterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
