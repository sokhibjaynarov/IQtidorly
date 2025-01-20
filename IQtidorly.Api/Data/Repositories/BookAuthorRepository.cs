using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Models.BookAuthors;

namespace IQtidorly.Api.Data.Repositories
{
    public class BookAuthorRepository : BaseRepository<BookAuthor, ApplicationDbContext>, IBookAuthorRepository
    {
        public BookAuthorRepository(ApplicationDbContext dbContext, IRequestLanguageHelper languageHelper) : base(dbContext, languageHelper)
        {
        }
    }
}
