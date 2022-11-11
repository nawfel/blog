using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace blog.services
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(String id);

    }
}
