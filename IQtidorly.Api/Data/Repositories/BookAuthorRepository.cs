using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.BookAuthors;

namespace IQtidorly.Api.Data.Repositories
{
    public class BookAuthorRepository : BaseRepository<BookAuthor, ApplicationDbContext>, IBookAuthorRepository
    {
        public BookAuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
