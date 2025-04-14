using Application.Interfaces.Services;
using Application.Reponse;
using Application.Request;
using Microsoft.AspNetCore.Mvc;

namespace FusionNews_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> SendMessage([FromBody] ChatRequest request)
        {
            var response = await _chatService.GetReplyAsync(request);
            return StatusCode((int)response.StatusCode, response);
        }
    }

}

