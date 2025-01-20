using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Models.Subjects;

namespace IQtidorly.Api.Data.Repositories
{
    public class SubjectRepository : BaseRepository<Subject, ApplicationDbContext>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext dbContext, IRequestLanguageHelper languageHelper) : base(dbContext, languageHelper)
        {
        }
    }
}
