using Application.Entities.Base;
using Application.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ICommentService
    {
        Task<APIResponse> GetCommentsAsync(int PostId);
        Task<APIResponse> CreateCommentAsync(Comment CommentModel);
        Task<APIResponse> GetCommentByIdAsync(int id);
        Task<APIResponse> UpdateCommentAsync(Comment CommentModel);
        Task<APIResponse> DeleteCommentAsync(int id);
    }
}
