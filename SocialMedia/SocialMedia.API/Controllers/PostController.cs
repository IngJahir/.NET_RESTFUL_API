using Microsoft.AspNetCore.Mvc;
using SocialMedia.INFRASTRUCTURE.Repositories;

namespace SocialMedia.API.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPosts()
        {
            var post = new PostRepository().GetPosts();
            return Ok(post);
        }
    }
}
