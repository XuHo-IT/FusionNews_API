using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface ICommentRepository
    {
        Task<Comment> CreateCommentAsync(Comment commentModel);
        Task<Comment> GetCommentByIdAsync(int id);
        Task<ICollection<Comment>> GetCommentsAsync(int postId);
        Task<Comment> UpdateCommentAsync(Comment commentModel);
        Task DeleteCommentAsync(int id);
    }
}
