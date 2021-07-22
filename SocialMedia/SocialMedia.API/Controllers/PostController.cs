namespace SocialMedia.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using SocialMedia.API.Responses;
    using SocialMedia.CORE.CustomEntities;
    using SocialMedia.CORE.DTOs;
    using SocialMedia.CORE.Entities;
    using SocialMedia.CORE.Interfaces;
    using SocialMedia.CORE.QueryFilters;
    using SocialMedia.INFRASTRUCTURE.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
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
        /// <summary>
        /// Deuelve todos los post
        /// </summary>
        /// <param name="filters"> Filtros para aplicar </param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPosts([FromQuery]PostQueryFilter filters)
        {
            var post = _postService.GetPosts(filters);
            var postDto = _mapper.Map<IEnumerable<PostDto>>(post);

            var metadata = new MetaData
            {
                TotalCount =  post.TotalCount,
                PageSize = post.PageSize,
                CurrentPage = post.CurrentPage,
                TotalPages = post.TotalPages,
                HasNextPage = post.HasNextPage,
                HasPreviousPage = post.HasPreviosPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString(),
                previousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
            };

            var response = new ApiResponse<IEnumerable<PostDto>>(postDto)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Paginacion",JsonConvert.SerializeObject(metadata));

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
