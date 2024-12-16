using IQtidorly.Api.ViewModels.Books;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IBookService
    {
        Task<(List<BookForGetModel> Books, int Count)> GetBooksAsPaginationAsync(int take, int skip);
        Task<BookForGetModel> GetBookByIdAsync(Guid bookId);
        Task<Guid> CreateBookAsync(BookForCreateModel bookForCreateModel);
        Task<bool> DeleteBookAsync(Guid bookId);
        Task<bool> UpdateBookAsync(BookForUpdateModel bookForUpdateModel);
    }
}
