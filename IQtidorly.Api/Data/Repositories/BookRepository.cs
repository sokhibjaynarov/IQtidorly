using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Models.Books;

namespace IQtidorly.Api.Data.Repositories
{
    public class BookRepository : BaseRepository<Book, ApplicationDbContext>, IBookRepository
    {
        public BookRepository(ApplicationDbContext dbContext, IRequestLanguageHelper languageHelper) : base(dbContext, languageHelper)
        {
        }
    }
}
