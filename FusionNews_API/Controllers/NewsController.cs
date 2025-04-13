using Application.Interfaces;
using Application.Reponse;
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
        public async Task<IActionResult> GetLatestNews()
        {
            try
            {
                var newsData = await _newsService.GetNewsAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                _response.Result = newsData;
                _log.LogiInfo("News fetched at " + DateTime.Now);
                return Ok(_response);
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

    }
}
