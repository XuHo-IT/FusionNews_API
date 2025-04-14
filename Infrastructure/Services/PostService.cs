﻿using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<IReadOnlyList<Post>> GetAllPosts()
        {
            return await _postRepository.GetAllPosts();
        }
    }
}
