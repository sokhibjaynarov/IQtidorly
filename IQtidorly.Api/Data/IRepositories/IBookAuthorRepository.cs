using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.BookAuthors;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IBookAuthorRepository : IBaseRepository<BookAuthor, ApplicationDbContext>
    {
    }
}
