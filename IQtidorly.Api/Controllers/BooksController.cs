using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using IQtidorly.Api.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("getall")]
        public async Task<JsonResponse> GetAllBooksAsPagination(int take, int skip)
        {
            try
            {
                var data = await _bookService.GetBooksAsPaginationAsync(take, skip);
                var response = new PaginationResponse(data.Books, skip, take, data.Count);
                return JsonResponse.DataResponse(response);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpGet("getbyid")]
        public async Task<JsonResponse> GetBookById(Guid bookId)
        {
            try
            {
                var data = await _bookService.GetBookByIdAsync(bookId);
                return JsonResponse.DataResponse(data);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("create")]
        public async Task<JsonResponse> CreateBook(BookForCreateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _bookService.CreateBookAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("update")]
        public async Task<JsonResponse> UpdateBook(BookForUpdateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _bookService.UpdateBookAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("delete")]
        public async Task<JsonResponse> DeleteBook(Guid bookId)
        {
            try
            {
                return JsonResponse.DataResponse(await _bookService.DeleteBookAsync(bookId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }
    }
}
