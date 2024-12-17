using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<JsonResponse> UploadFileAsync(IFormFile file)
        {
            try
            {
                return JsonResponse.DataResponse(await _fileService.UploadFileAsync(file));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFileAsync(Guid fileId)
        {
            try
            {
                var file = await _fileService.GetFileByIdAsync(fileId);
                return File(file.Content, file.MimeType, file.Name, enableRangeProcessing: true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("save")]
        public async Task<JsonResponse> SaveFileAsync(Guid fileId)
        {
            try
            {
                return JsonResponse.DataResponse(await _fileService.SaveFileAsync(fileId));
            }
            catch (Exception ex)
            {
                return JsonResponse.ErrorResponse(ex);
            }
        }
    }
}
