namespace SocialMedia.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using SocialMedia.CORE.DTOs;
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var post = await _postRepository.GetPosts();
            var postDto = _mapper.Map<IEnumerable<PostDto>>(post);
            return Ok(postDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosts(int id)
        {
            var post = await _postRepository.GetPosts(id);
            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _postRepository.InsertPost(post);
            return Ok(post);
        }
    }
}
