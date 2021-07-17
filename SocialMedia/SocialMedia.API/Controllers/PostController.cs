namespace SocialMedia.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using SocialMedia.API.Responses;
    using SocialMedia.CORE.DTOs;
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        // CREATE 
        // ------
        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            // if (!ModelState.IsValid) { return BadRequest(ModelState);}
            // Este condicional requiere un data anotation.

            var post = _mapper.Map<Post>(postDto);
            await _postService.InsertPost(post);

            postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);

            return Ok(response);
        }

        // READ
        // ----
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var post = await _postService.GetPosts();
            var postDto = _mapper.Map<IEnumerable<PostDto>>(post);
            var response = new ApiResponse<IEnumerable<PostDto>>(postDto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosts(int id)
        {
            var post = await _postService.GetPosts(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }
                
        // UPDATE
        // ------
        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.Id = id;

            var result = await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        // DELETE
        // ------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
