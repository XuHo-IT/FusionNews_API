using Application.Entities.Base;
using Application.Entities.DTOS.ChatBotQuestion;
using AutoMapper;
using FusionNews_API.DTOs.Post;

namespace FusionNews_API.Helpers
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Post, CreatePostDto>().ReverseMap();
            CreateMap<ChatbotQuestion, ChatbotQuestionCreateDTO>().ReverseMap();
        }
    }
}
