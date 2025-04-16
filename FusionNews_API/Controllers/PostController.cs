using Application.Entities.Base;
using Application.Interfaces.IServices;
using Application.Interfaces.Services;
using Application.Reponse;
using AutoMapper;
using FusionNews_API.DTOs.Post;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FusionNews_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly APIResponse _response;
        private readonly ILogService _log;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, ILogService log, IMapper mapper)
        {
            _postService = postService;
            _response = new APIResponse();
            _log = log;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var result = await _postService.GetAllPosts();

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostDto postCreateDto)
        {
            try
            {
                Post postmodel = _mapper.Map<Post>(postCreateDto);
                var response = await _postService.CreatePost(postmodel);
                _log.LogiInfo($"Post created successfully at {DateTime.Now}. PostId: {postCreateDto.NewsOfPostId}");

                return StatusCode((int)response.StatusCode, response);
            }
            catch (HttpRequestException ex)
            {
                // Handling HTTP request errors (e.g., API issues)
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.isSuccess = false;
                _response.ErrorMessages.Add($"News API error: {ex.Message}");

                _log.LogError($"News fetch failed at {DateTime.Now}. Error: {ex.Message}");

                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                // Catching unexpected errors
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.isSuccess = false;
                _response.ErrorMessages.Add($"Unexpected error: {ex.Message}");

                _log.LogError($"Unexpected error during post creation at {DateTime.Now}. Error: {ex.Message}");

                return StatusCode(500, _response);
            }
        }

    }
}
