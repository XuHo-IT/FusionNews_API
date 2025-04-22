using Application.Interfaces.Services;
using Application.Reponse;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FusionNews_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly APIResponse _response;
        private readonly ILogService _log;
        public NewsController(INewsService newsService, ILogService log)
        {
            _newsService = newsService;
            _response = new APIResponse();
            _log = log;
        }
        [HttpGet("get-all-news")]
        public async Task<IActionResult> GetLatestNews([FromQuery] string? filterOn, [FromQuery] string? filterRequest, [FromQuery] int pageNumber = MyConstants.pageNumber, [FromQuery] int pageSize = MyConstants.pageSize)
        {
            try
            {
                var response = await _newsService.GetNewsAsync(filterOn, filterRequest, pageNumber, pageSize);

                _response.StatusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                _response.Result = response;
                _log.LogiInfo("News fetched at " + DateTime.Now);
                return StatusCode((int)_response.StatusCode, _response);
            }
            catch (HttpRequestException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.isSuccess = false;
                _response.ErrorMessages.Add("News API error: " + ex.Message);
                _log.LogError("News fetched fail at " + DateTime.Now);
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.isSuccess = false;
                _response.ErrorMessages.Add("Unexpected error: " + ex.Message);
                _log.LogError("News fetched fail at " + DateTime.Now);
                return StatusCode(500, _response);
            }
        }

        [HttpGet("get-news-by-article-id")]
        public async Task<ActionResult> GetNewsById(string articleId)
        {
            return Ok();
        }

        [HttpGet("get-news-by-id-in-database")]
        public async Task<ActionResult> GetNewsById(int NewsOfPostId)
        {
            return Ok();
        }
    }
}
