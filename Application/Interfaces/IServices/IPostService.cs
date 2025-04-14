using Application.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IPostService
    {
        Task<IReadOnlyList<Post>> GetAllPosts();
    }
}
