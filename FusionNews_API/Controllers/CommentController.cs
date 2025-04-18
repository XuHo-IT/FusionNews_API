using Application.Entities.Base;
using Application.Entities.DTOS.CommentOfPost;
using Application.Entities.DTOS.Post;
using Application.Interfaces.IServices;
using Application.Interfaces.Services;
using Application.Reponse;
using AutoMapper;
using FusionNews_API.DTOs.Post;
using FusionNews_API.Services.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FusionNews_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly ILogService _log;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService, IPostService postService, ILogService log, IMapper mapper)
        {
            _commentService = commentService;
            _postService = postService;
            _response = new APIResponse();
            _log = log;
            _mapper = mapper;
        }

        [HttpGet("get-all-comment")]
        public async Task<IActionResult> GetAllComments(int PostId)
        {
            try
            {
                var post = await _postService.GetPostByIdAsync(PostId);

                if (post == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    _response.ErrorMessages.Add("Post not found");
                    return NotFound(_response);
                }

                var response = await _commentService.GetCommentsAsync(PostId);
                _log.LogiInfo($"Get Comments successfully at {DateTime.Now}.");

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

                _log.LogError($"Unexpected error during get comments at {DateTime.Now}. Error: {ex.Message}");

                return StatusCode(500, _response);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateComment([FromBody] CreateComment commentCreateDto)
        {
            try
            {
                Comment commentModel = _mapper.Map<Comment>(commentCreateDto);
                var response = await _commentService.CreateCommentAsync(commentModel);
                _log.LogiInfo($"Comment created successfully at {DateTime.Now}.");

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

                _log.LogError($"Unexpected error during comment creation at {DateTime.Now}. Error: {ex.Message}");

                return StatusCode(500, _response);
            }
        }

        [HttpGet("get-comment-by-id/{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            try
            {
                var result = await _commentService.GetCommentByIdAsync(id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    _response.ErrorMessages.Add("Comment not found");
                    return NotFound(_response);
                }

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
                _log.LogError($"Unexpected error during comment get by id at {DateTime.Now}. Error: {ex.Message}");
                return StatusCode(500, _response);
            }
        }

        [HttpPut("update-comment")]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateComment updateCommentDto)
        {
            try
            {
                var result = await _commentService.GetCommentByIdAsync(updateCommentDto.CommentId);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    _response.ErrorMessages.Add("Comment not found");
                    return NotFound(_response);
                }

                Comment commentModel = _mapper.Map<Comment>(updateCommentDto);
                var respone = await _commentService.UpdateCommentAsync(commentModel);

                _response.StatusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                _response.Result = respone;
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
                _log.LogError($"Unexpected error during comment update at {DateTime.Now}. Error: {ex.Message}");
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("delete-post/{id:int}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            try
            {
                var result = await _commentService.GetCommentByIdAsync(id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    _response.ErrorMessages.Add("Post not found");
                    return NotFound(_response);
                }

                var postDelete = await _commentService.DeleteCommentAsync(id);

                _response.StatusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                _response.Result = postDelete;
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
                _log.LogError($"Unexpected error during comment delete at {DateTime.Now}. Error: {ex.Message}");
                return StatusCode(500, _response);
            }
        }
    }
}
