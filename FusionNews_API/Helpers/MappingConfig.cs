using Application.Entities.Base;
using Application.Entities.DTOS.ChatBotQuestion;
using Application.Entities.DTOS.CommentOfPost;
using Application.Entities.DTOS.Post;
using Application.Entities.DTOS.User;
using Application.Request.Chatbot;
using AutoMapper;
using FusionNews_API.DTOs.Post;

namespace FusionNews_API.Helpers
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Post, CreatePostDto>().ReverseMap();
            CreateMap<Post, UpdatePostDto>().ReverseMap();
            CreateMap<Comment, CreateComment>().ReverseMap();
            CreateMap<Comment, UpdateComment>().ReverseMap();
            CreateMap<ChatbotQuestion, ChatbotQuestionCreateDTO>().ReverseMap();
            CreateMap<ChatbotQuestion, ChatbotQuestionUpdateDTO>().ReverseMap();
            CreateMap<ChatbotQuestion, ChatbotQuestionRequest>().ReverseMap();
            CreateMap<ChatbotQuestion, ChatbotAnswerRequest>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();

        }
    }
}
