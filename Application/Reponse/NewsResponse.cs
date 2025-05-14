using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.Base;

namespace Application.Reponse
{
    public class NewsResponse
    {
        public NewsApiResponse NewsApiResponse { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults
        {
            get
            {
                return NewsApiResponse?.TotalResults ?? 0;
            }
        }
    }
}
