﻿using Application.Interfaces.IRepositories;
using Application.Interfaces.Services;
using Application.Reponse;
using System.Net;

namespace FusionNews_API.Services.News
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<APIResponse> GetNewsAsync()
        {
            var response = new APIResponse();

            try
            {
                var articles = await _newsRepository.FetchNewsAsync();

                response.Result = articles;
                response.StatusCode = HttpStatusCode.OK;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add(ex.Message);
            }

            return response;
        }
    }
}
