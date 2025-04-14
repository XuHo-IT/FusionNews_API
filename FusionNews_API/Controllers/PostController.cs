using Application.Entities.Base;
using Application.Interfaces.IServices;
using FusionNews_API.DTOs.Post;
using FusionNews_API.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FusionNews_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Post>>> GetPosts()
        {
            var posts = await _postService.GetAllPosts(); 

            return Ok(posts.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] CreatePostDto postDto)
        {
            var postModel = postDto.ToPostFromCreate();
            await _postService.CreatePost(postModel);

            //return CreatedAtAction(nameof(GetPosts), new { id = post.PostId }, post);
            return Ok(postModel);
        }
    }
}
