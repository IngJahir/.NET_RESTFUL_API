﻿namespace SocialMedia.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var post = await _postRepository.GetPosts();
            return Ok(post);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosts(int id)
        {
            var post = await _postRepository.GetPosts(id);
            return Ok(post);
        }


        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            await _postRepository.InsertPost(post);
            return Ok(post);
        }




    }
}
