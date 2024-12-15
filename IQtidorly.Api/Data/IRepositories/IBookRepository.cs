using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.Books;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IBookRepository : IBaseRepository<Book, ApplicationDbContext>
    {
    }
}
