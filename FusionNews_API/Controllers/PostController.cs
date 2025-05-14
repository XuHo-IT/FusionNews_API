using Application.Entities.Base;
using Application.Entities.DTOS.Comment;
using Application.Entities.DTOS.Post;
using Application.Interfaces.IServices;
using Application.Interfaces.Services;
using Application.Reponse;
using AutoMapper;
using FusionNews_API.DTOs.Post;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("get-all-post")]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var response = await _postService.GetPostsAsync();
                _log.LogiInfo($"Get Pots successfully at {DateTime.Now}.");

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

                _log.LogError($"Unexpected error during get posts at {DateTime.Now}. Error: {ex.Message}");

                return StatusCode(500, _response);
            }
        }

        [HttpPost("create-post")]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostDto postCreateDto)
        {
            try
            {
                //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

                //if (string.IsNullOrEmpty(userId))
                //{
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    _response.isSuccess = false;
                //    _response.ErrorMessages.Add("User not found");
                //    return BadRequest(_response);
                //}

                Post postmodel = _mapper.Map<Post>(postCreateDto);
                //postmodel.UserId = userId; 
                postmodel.UserId = "545400cb-fbef-4dd5-a306-7953935d1885"; // test

                var response = await _postService.CreatePostAsync(postmodel);
                _log.LogiInfo($"Post created successfully at {DateTime.Now}.");

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

        [HttpGet("get-post-by-id/{id:int}")]
        public async Task<IActionResult> GetPostById([FromRoute] int id)
        {
            try
            {
                var result = await _postService.GetPostByIdAsync(id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    _response.ErrorMessages.Add("Post not found");
                    return NotFound(_response);
                }

                //////////////////////////////////////////////////////////////////////
                // Test with static user
                //var postDto = _mapper.Map<PostDto>(post);
                //postDto.CreatedBy = "Tuan vip pro"; // test
                //var commentDto = _mapper.Map<List<CommentDto>>(postDto.Comments);
                //foreach (var comment in commentDto)
                //{
                //    comment.CreatedBy = "Tuan vip pro"; // test
                //}

                //postDto.Comments = commentDto;
                //////////////////////////////////////////////////////////////////////

                _response.StatusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                _response.Result = result;
                return Ok(_response);
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
                _log.LogError($"Unexpected error during post get by id at {DateTime.Now}. Error: {ex.Message}");
                return StatusCode(500, _response);
            }
        }

        [HttpPut("update-post")]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostDto updatePostDto)
        {
            try
            {
                var result = await _postService.GetPostByIdAsync(updatePostDto.PostId);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    _response.ErrorMessages.Add("Post not found");
                    return NotFound(_response);
                }

                Post postmodel = _mapper.Map<Post>(updatePostDto);
                var postUpdate = await _postService.UpdatePostAsync(postmodel);

                _response.StatusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                _response.Result = result;
                return Ok(_response);
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
                _log.LogError($"Unexpected error during post update at {DateTime.Now}. Error: {ex.Message}");
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("delete-post/{id:int}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            try
            {
                var result = await _postService.GetPostByIdAsync(id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    _response.ErrorMessages.Add("Post not found");
                    return NotFound(_response);
                }

                var postDelete = await _postService.DeletePostAsync(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                _response.Result = result;
                return Ok(_response);
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
                _log.LogError($"Unexpected error during post delete at {DateTime.Now}. Error: {ex.Message}");
                return StatusCode(500, _response);
            }
        }
    }
}
