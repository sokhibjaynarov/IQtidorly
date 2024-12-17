using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Response;
using IQtidorly.Api.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;
using File = IQtidorly.Api.Models.Files.File;

namespace IQtidorly.Api.Services
{
    public class FileService : BaseService, IFileService
    {
        public FileService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<FileService> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task<Guid> UploadFileAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new ErrorCodeException(ResponseCodes.ERROR_INVALID_DATA);
                }

                var currentUserId = WebHelper.GetUserId(_httpContextAccessor.HttpContext);

                byte[] fileData;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileData = memoryStream.ToArray();
                }

                Path.GetExtension(file.FileName);

                var newFile = new File
                {
                    Name = file.FileName,
                    Extension = Path.GetExtension(file.FileName),
                    MimeType = MimeTypes.GetMimeType(file.FileName),
                    CreatedById = currentUserId,
                    Content = fileData,
                    IsTemporary = true
                };

                newFile = await _unitOfWorkRepository.FileRepository.AddAsync(newFile);

                if (await _unitOfWorkRepository.FileRepository.SaveChangesAsync() > 0)
                {
                    return newFile.FileId;
                }

                throw new ErrorCodeException(ResponseCodes.ERROR_SAVE_DATA);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> SaveFileAsync(Guid fileId)
        {
            try
            {
                var file = await _unitOfWorkRepository.FileRepository.GetAsync(fileId);

                if (file == null)
                {
                    throw new ErrorCodeException(ResponseCodes.ERROR_NOT_FOUND_DATA);
                }

                file.IsTemporary = false;

                _unitOfWorkRepository.FileRepository.Update(file);

                if (await _unitOfWorkRepository.FileRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new ErrorCodeException(ResponseCodes.ERROR_SAVE_DATA);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<File> GetFileByIdAsync(Guid fileId)
        {
            try
            {
                var file = await _unitOfWorkRepository.FileRepository.GetAsync(fileId);

                if (file == null)
                {
                    throw new ErrorCodeException(ResponseCodes.ERROR_NOT_FOUND_DATA);
                }

                return file;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
