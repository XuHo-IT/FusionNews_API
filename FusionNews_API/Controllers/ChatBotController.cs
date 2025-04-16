using Application.Entities.Base;
using Application.Entities.DTOS.ChatBotQuestion;
using Application.Interfaces.IServices;
using Application.Interfaces.Services;
using Application.Reponse;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FusionNews_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly IChatBotService _chatBotService;
        private readonly APIResponse _response;
        private readonly ILogService _log;
        private readonly IMapper _mapper;

        public ChatBotController(IChatBotService chatBotService, ILogService log, IMapper mapper)
        {
            _chatBotService = chatBotService;
            _response = new APIResponse();
            _log = log;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> CreateQuestion([FromBody] ChatbotQuestionCreateDTO chatbotQuestionCreateDTO)
        {
            try
            {
                ChatbotQuestion chatbotQuestion = _mapper.Map<ChatbotQuestion>(chatbotQuestionCreateDTO);
                var response = await _chatBotService.CreateQuestion(chatbotQuestion);
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
        [HttpGet("get-all-question")]
        public async Task<IActionResult> GetLatestNews()
        {
            try
            {
                var response = await _chatBotService.GetAllQuetions();
                _log.LogiInfo("News fetched at " + DateTime.Now);
                return StatusCode((int)response.StatusCode, response);
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
