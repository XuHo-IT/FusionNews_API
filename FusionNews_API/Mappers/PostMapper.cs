using Application.Entities.Base;
using FusionNews_API.DTOs.Post;

namespace FusionNews_API.Mappers
{
    public static class PostMapper
    {
        public static PostDto ToPostDto(this Post postModel)
        {
            return new PostDto
            {
                PostId = postModel.PostId,
                Title = postModel.Title,
                Content = postModel.Content,
                NewsOfPostId = null,
                CreatedOn = postModel.CreatedOn,
                NewsOfPost = null,
                Comments = [],
                PostTags = null
            };
        }

        public static Post ToPostFromCreate(this CreatePostDto postDto)
        {
            return new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
            };
        }   
    }
}
