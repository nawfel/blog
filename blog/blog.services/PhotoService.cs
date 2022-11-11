using blog.models.settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinaryOptions> config)
        {
            var account = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadRes = new ImageUploadResult();
            if (file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(300).Width(500).Crop("fill")
                    };
                    uploadRes = await _cloudinary.UploadAsync(uploadParams);
                }
            }
            return uploadRes;
        }

        public Task<DeletionResult> DeletePhotoAsync(string id)
        {
            var deleteParams = new DeletionParams(id);
            var res = _cloudinary.DestroyAsync(deleteParams);
            return res;
        }
    }
}
