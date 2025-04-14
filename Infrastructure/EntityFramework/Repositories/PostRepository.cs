using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.Repositories
{
    public class PostRepository : IPostRepository
    {
        public Task<IReadOnlyList<Post>> GetAllPosts()
        {
            throw new NotImplementedException();
        }
    }
}
