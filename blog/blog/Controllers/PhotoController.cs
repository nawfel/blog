using blog.models.photo;
using blog.repository;
using blog.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace blog.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IPhotoService _photoService;

        public PhotoController(
            IPhotoRepository photoRepository,
            IBlogRepository blogRepository,
            IPhotoService photoService)
        {
            _photoRepository = photoRepository;
            _blogRepository = blogRepository;
            _photoService = photoService;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Photo>> UploadPhoto(IFormFile file)
        {
            int applicationUserId = int.Parse(User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);

            var uploadRes = await _photoService.AddPhotoAsync(file);
            if (uploadRes.Error != null) return BadRequest(uploadRes.Error.Message);

            var photoCreate = new PhotoCreate
            {
                PublicId = uploadRes.PublicId,
                ImageUrl = uploadRes.SecureUrl.AbsoluteUri,
                Description = file.Name,
            };
            var photo = await _photoRepository.InsertAsync(photoCreate, applicationUserId);
            return Ok(photo);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Photo>>> GetByApplicationUserId()
        {
            int applicationUserId = int.Parse(User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
            var photos = await _photoRepository.GetAllByUserIdAsync(applicationUserId);
            return Ok(photos); 
        }

        [HttpGet("{photoId}")]
        public async Task<ActionResult<Photo>> Get(int photoId)
        {
            var photo = await _photoRepository.GetAsync(photoId);
            return Ok(photo);  
        }


        [Authorize]
        [HttpDelete("{photoId}")]

        public async Task<ActionResult<int>> Delete(int photoId)
        {
            int applicationUserId = int.Parse(User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
            var photofound = await _photoRepository.GetAsync(photoId);
            if(photofound != null)
            {
                if (photofound.ApplicationUserId == applicationUserId)
                {
                    var blogs = await _blogRepository.GetAllByUserIdAsync(applicationUserId);

                    var usedInBlog = blogs.Any(x => x.PhotoId == photoId);
                    if (usedInBlog) return BadRequest("cannot remove photo as it is being used in published blog(s)");
                    var deleteRes = await _photoService.DeletePhotoAsync(photofound.PublicId);
                    if(deleteRes.Error !=null) return BadRequest(deleteRes.Error.Message);

                    var affectedRows = await _photoRepository.DeleteAsync(photofound.PhotoId);
                    return Ok(affectedRows);
                }
                else
                {
                    return BadRequest("photo was not uploaded by the current user");
                }
            }
            return BadRequest("Photo does not exist");
        }
    }
}
