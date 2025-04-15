using Application.Entities.Base;
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
        }
    }
}
