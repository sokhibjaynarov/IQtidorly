using IQtidorly.Api.ViewModels.BookAuthors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IBookAuthorService
    {
        Task<(List<BookAuthorForGetModel> BookAuthors, int Count)> GetBookAuthorsAsPaginationAsync(int take, int skip);
        Task<BookAuthorForGetModel> GetBookAuthorByIdAsync(Guid bookAuthorId);
        Task<Guid> CreateBookAuthorAsync(BookAuthorForCreateModel bookAuthorForCreateModel);
        Task<bool> DeleteBookAuthorAsync(Guid bookAuthorId);
        Task<bool> UpdateBookAuthorAsync(BookAuthorForUpdateModel bookAuthorForUpdateModel);
    }
}
