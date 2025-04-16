using Application.Entities.Base;
using Application.Entities.DTOS.User;
using AutoMapper;
using FusionNews_API.DTOs.Post;

namespace FusionNews_API.Helpers
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Post, CreatePostDto>();
            CreateMap<Post, CreatePostDto>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }
    }
}
