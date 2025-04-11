using FusionNews_API.Entities;
using FusionNews_API.Interfaces.News;
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

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
            _response = new APIResponse();
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

                return Ok(_response);
            }
            catch (HttpRequestException ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.isSuccess = false;
                _response.ErrorMessages.Add("News API error: " + ex.Message);
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.isSuccess = false;
                _response.ErrorMessages.Add("Unexpected error: " + ex.Message);
                return StatusCode(500, _response);
            }
        }

    }
}
