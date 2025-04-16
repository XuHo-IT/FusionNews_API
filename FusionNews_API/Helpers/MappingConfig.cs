using Application.Entities.Base;
using Application.Entities.DTOS.ChatBotQuestion;
using Application.Entities.DTOS.User;
using Application.Entities.DTOS.Post;
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
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDto>();
            CreateMap<Post, UpdatePostDto>().ReverseMap();
        }
    }
}
