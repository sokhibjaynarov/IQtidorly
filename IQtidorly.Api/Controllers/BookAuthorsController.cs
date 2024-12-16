using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using IQtidorly.Api.ViewModels.BookAuthors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/book-authors")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IBookAuthorService _bookAuthorService;

        public BookAuthorsController(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }

        [HttpGet("getall")]
        public async Task<JsonResponse> GetAllBookAuthorsAsPagination(int take, int skip)
        {
            try
            {
                var data = await _bookAuthorService.GetBookAuthorsAsPaginationAsync(take, skip);
                var response = new PaginationResponse(data.BookAuthors, skip, take, data.Count);
                return JsonResponse.DataResponse(response);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpGet("getbyid")]
        public async Task<JsonResponse> GetBookAuthorById(Guid bookAuthorId)
        {
            try
            {
                var data = await _bookAuthorService.GetBookAuthorByIdAsync(bookAuthorId);
                return JsonResponse.DataResponse(data);
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("create")]
        public async Task<JsonResponse> CreateBookAuthor(BookAuthorForCreateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _bookAuthorService.CreateBookAuthorAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("delete")]
        public async Task<JsonResponse> DeleteBookAuthor(Guid bookAuthorId)
        {
            try
            {
                return JsonResponse.DataResponse(await _bookAuthorService.DeleteBookAuthorAsync(bookAuthorId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpPost("update")]
        public async Task<JsonResponse> UpdateBookAuthor(BookAuthorForUpdateModel viewModel)
        {
            try
            {
                return JsonResponse.DataResponse(await _bookAuthorService.UpdateBookAuthorAsync(viewModel));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }
    }
}
