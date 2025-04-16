using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post> CreatePostAsync(Post postModel);
        Task<Post> GetPostByIdAsync(int id);
        Task<Post> UpdatePostAsync(Post postModel);
        Task<Post> DeletePostAsync(int id);
    }
}
