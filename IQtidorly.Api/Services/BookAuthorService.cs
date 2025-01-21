using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.BookAuthors;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.BookAuthors;
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
    public class BookAuthorService : BaseService, IBookAuthorService
    {
        public BookAuthorService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<BookAuthorService> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task<Guid> CreateBookAuthorAsync(BookAuthorForCreateModel bookAuthorForCreateModel)
        {
            try
            {
                var exitingBookAuthor = await _unitOfWorkRepository.BookAuthorRepository
                    .GetAll().FirstOrDefaultAsync(x => x.FirstName == bookAuthorForCreateModel.FirsName &&
                        x.LastName == bookAuthorForCreateModel.LastName);

                if (exitingBookAuthor != null)
                {
                    throw new Exception("Allready exist");
                }

                var bookAuthor = _mapper.Map<BookAuthor>(bookAuthorForCreateModel);

                bookAuthor = await _unitOfWorkRepository.BookAuthorRepository.AddAsync(bookAuthor);

                if (await _unitOfWorkRepository.BookAuthorRepository.SaveChangesAsync() > 0)
                {
                    return bookAuthor.BookAuthorId;
                }

                throw new Exception("error creating book author");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteBookAuthorAsync(Guid bookAuthorId)
        {
            try
            {
                var bookAuthor = await _unitOfWorkRepository.BookAuthorRepository
                    .GetAll().Include(p => p.Books).FirstOrDefaultAsync(x => x.BookAuthorId == bookAuthorId);

                if (bookAuthor == null)
                {
                    throw new Exception("Book author not found");
                }

                if (bookAuthor.Books.Any())
                {
                    throw new Exception("Book author has books");
                }

                _unitOfWorkRepository.BookAuthorRepository.Remove(bookAuthor);

                if (await _unitOfWorkRepository.BookAuthorRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("error deleting book author");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<BookAuthorForGetModel> GetBookAuthorByIdAsync(Guid bookAuthorId)
        {
            try
            {
                var bookAuthor = await _unitOfWorkRepository.BookAuthorRepository
                    .GetAll().Include(p => p.Books).FirstOrDefaultAsync(x => x.BookAuthorId == bookAuthorId);

                if (bookAuthor == null)
                {
                    throw new Exception("Book author not found");
                }

                var bookAuthorViewModel = _mapper.Map<BookAuthorForGetModel>(bookAuthor);

                return bookAuthorViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<(List<BookAuthorForGetModel> BookAuthors, int Count)> GetBookAuthorsAsPaginationAsync(int take, int skip)
        {
            try
            {
                var bookAuthorsQuery = _unitOfWorkRepository.BookAuthorRepository.GetAll();

                var bookAuthors = await bookAuthorsQuery.Skip(skip).Take(take).ToListAsync();
                var count = await bookAuthorsQuery.CountAsync();

                if (!bookAuthors.Any())
                {
                    return (new List<BookAuthorForGetModel>(), count);
                }

                var bookAuthorsViewModel = _mapper.Map<List<BookAuthorForGetModel>>(bookAuthors);

                return (bookAuthorsViewModel, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateBookAuthorAsync(BookAuthorForUpdateModel bookAuthorForUpdateModel)
        {
            try
            {
                var existingBookAuthor = await _unitOfWorkRepository.BookAuthorRepository
                    .GetAll().FirstOrDefaultAsync(x => x.BookAuthorId == bookAuthorForUpdateModel.BookAuthorId);

                if (existingBookAuthor == null)
                {
                    throw new Exception("Book author not found");
                }

                existingBookAuthor = _mapper.Map(bookAuthorForUpdateModel, existingBookAuthor);

                if (await _unitOfWorkRepository.BookAuthorRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("error updating book author");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
