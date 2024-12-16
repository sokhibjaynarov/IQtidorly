using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.Books;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IQtidorly.Api.Services
{
    public class BookService : BaseService, IBookService
    {
        public BookService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<BookService> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task<Guid> CreateBookAsync(BookForCreateModel bookForCreateModel)
        {
            try
            {
                var existingBook = await _unitOfWorkRepository.BookRepository
                    .GetAll().FirstOrDefaultAsync(x => x.Title == bookForCreateModel.Title);

                if (existingBook != null)
                {
                    throw new Exception("Allready exist");
                }

                var existBookAuthor = await _unitOfWorkRepository.BookAuthorRepository
                    .GetAll().FirstOrDefaultAsync(x => x.BookAuthorId == bookForCreateModel.BookAuthorId);

                if (existBookAuthor == null)
                {
                    throw new Exception("Author not found");
                }

                var book = _mapper.Map<Book>(bookForCreateModel);

                book = await _unitOfWorkRepository.BookRepository.AddAsync(book);

                if (await _unitOfWorkRepository.BookRepository.SaveChangesAsync() > 0)
                {
                    return book.BookId;
                }

                throw new Exception("error creating book");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteBookAsync(Guid bookId)
        {
            try
            {
                var book = await _unitOfWorkRepository.BookRepository.GetAsync(bookId);

                if (book == null)
                {
                    throw new Exception("Book not found");
                }

                _unitOfWorkRepository.BookRepository.Remove(book);

                if (await _unitOfWorkRepository.BookRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("error deleting book");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<BookForGetModel> GetBookByIdAsync(Guid bookId)
        {
            try
            {
                var book = await _unitOfWorkRepository.BookRepository.GetAsync(bookId);

                if (book == null)
                {
                    throw new Exception("Book not found");
                }

                var bookForGetModel = _mapper.Map<BookForGetModel>(book);

                return bookForGetModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<(List<BookForGetModel> Books, int Count)> GetBooksAsPaginationAsync(int take, int skip)
        {
            try
            {
                var booksQuery = _unitOfWorkRepository.BookRepository.GetAll();

                var books = await booksQuery.Skip(skip).Take(take).ToListAsync();
                var count = await booksQuery.CountAsync();

                if (!books.Any())
                {
                    return (new List<BookForGetModel>(), count);
                }

                var booksForGetModel = _mapper.Map<List<BookForGetModel>>(books);

                return (booksForGetModel, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateBookAsync(BookForUpdateModel bookForUpdateModel)
        {
            try
            {
                var existingBook = await _unitOfWorkRepository.BookRepository
                    .GetAll().FirstOrDefaultAsync(x => x.BookId == bookForUpdateModel.BookId);

                if (existingBook != null)
                {
                    throw new Exception("Book not found");
                }

                var existBookAuthor = await _unitOfWorkRepository.BookAuthorRepository
                    .GetAll().FirstOrDefaultAsync(x => x.BookAuthorId == bookForUpdateModel.BookAuthorId);

                if (existBookAuthor == null)
                {
                    throw new Exception("Author not found");
                }

                existingBook = _mapper.Map<Book>(bookForUpdateModel);

                _unitOfWorkRepository.BookRepository.Update(existingBook);

                if (_unitOfWorkRepository.BookRepository.SaveChangesAsync().Result > 0)
                {
                    return true;
                }

                throw new Exception("error updating book");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
