using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IFileService
    {
        Task<Guid> UploadFileAsync(IFormFile file);

        Task<bool> SaveFileAsync(Guid fileId);
    }
}
