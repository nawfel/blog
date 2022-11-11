using blog.models.blog;
using blog.repository;
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
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IPhotoRepository _photoRepository;

        public BlogController(IBlogRepository blogRepository, IPhotoRepository photoRepository)
        {
            _blogRepository = blogRepository;
            _photoRepository = photoRepository;
        }

       
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Blog>> Create(BlogCreate blogCreate)
        {
            int applicationUserId = int.Parse(User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
            if (blogCreate.PhotoId.HasValue)
            {
                var photo = await _photoRepository.GetAsync(blogCreate.PhotoId.Value);
                if(photo.ApplicationUserId != applicationUserId)
                {
                    return BadRequest("you did not upload the photo");
                }
            }
            var blog = _blogRepository.UpsertAsync(blogCreate, applicationUserId);
            return Ok(blog);
        }

     
        
        [HttpGet]
        public async Task<ActionResult<PagedResults<Blog>>> GetAll([FromQuery] BlogPaging blogpaging)
        {
            var blogs = await _blogRepository.GetAllAsync(blogpaging);
            return Ok(blogs);
        }

      
        
        [HttpGet("{blogId}")]
        public async Task<ActionResult<Blog>> Get(int blogId)
        {
            var blog = await _blogRepository.GetASync(blogId);
            return Ok(blog);
        }

      
        
        [HttpGet("user/{applicationUserId}")]
        public async Task<ActionResult<List<Blog>>> GetByAppUserId(int applicationUserId)
        {
            var blogs= await _blogRepository.GetAllByUserIdAsync(applicationUserId);
            return Ok(blogs);
        }

        [HttpGet("famous")]
        public async Task<ActionResult<List<Blog>>> GetAllFamous()
        {
            var blogs = await _blogRepository.GetAllFamousAsync();
            return Ok(blogs);
        }
        [Authorize]
        [HttpDelete("{blogId}")]
        public async Task<ActionResult<List<Blog>>> Delete(int blogId)
        {
            int applicationUserId = int.Parse(User.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundBlog = await _blogRepository.GetASync(blogId);
            if (foundBlog == null) return BadRequest("Blog does not exist");
            if(foundBlog.ApplicationUserId== applicationUserId)
            {
                var res = await _blogRepository.DeleteAsync(blogId);
                return Ok(res);
            }
            else
            {
                return BadRequest("you did not create this blog");
            }
            
        }
    }
}
