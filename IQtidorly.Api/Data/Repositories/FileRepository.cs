using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Models.Files;

namespace IQtidorly.Api.Data.Repositories
{
    public class FileRepository : BaseRepository<File, ApplicationDbContext>, IFileRepository
    {
        public FileRepository(ApplicationDbContext dbContext, IRequestLanguageHelper languageHelper) : base(dbContext, languageHelper)
        {
        }
    }
}
