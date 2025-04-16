using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Reponse;
using System.Net;

namespace FusionNews_API.Services.Quetions
{
    public class ChatBotService : IChatBotService
    {
        private readonly IChatBotRepository _chatBotRepository;

        public ChatBotService(IChatBotRepository chatBotRepository)
        {
            _chatBotRepository = chatBotRepository;
        }

        public async Task<APIResponse> CreateQuestion(ChatbotQuestion chatbotQuestion)
        {
            var response = new APIResponse();

            try
            {
                await _chatBotRepository.CreateQuestion(chatbotQuestion);

                response.Result = "Question created successfully";
                response.StatusCode = HttpStatusCode.OK;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
            throw new NotImplementedException();
        }

        public async Task<APIResponse> GetAllQuetions()
        {
            var response = new APIResponse();

            try
            {
                var resultResponse = await _chatBotRepository.GetQuestion();

                response.Result = resultResponse;
                response.StatusCode = HttpStatusCode.OK;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
            throw new NotImplementedException();
        }
    }
}
