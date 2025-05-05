using Application.Entities.Base;
using Application.Reponse;

namespace FusionNews_API.Helpers
{
    public class Util
    {
        //public static NewsResponse Filtering(NewsResponse newsApiResponse, string? filterOn = null, string? filterRequest = null)
        //{
        //    if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterRequest))
        //    {
        //        //if (filterOn.Equals("Country", StringComparison.OrdinalIgnoreCase))
        //        //{
        //        //    articles = articles.Where(a => a.Country.Any(c => c.ToUpper().Contains(filterRequest.ToUpper()))).ToList();
        //        //}
        //        //if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
        //        //{
        //        //    articles = articles.Where(a => a.Category.Any(c => c.ToUpper().ToUpper().Contains(filterRequest.ToUpper()))).ToList();
        //        //}
        //        if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
        //        {
        //            articles = articles.Where(a => a.Title.ToUpper().Contains(filterRequest.ToUpper())).ToList();
        //        }
        //    }

        //    return articles;
        //}

        public static NewsResponse Pagination(int pageNumber, int pageSize, NewsResponse newsApiResponse)
        {
            var skip = (pageNumber - 1) * pageSize;
            newsApiResponse.NewsApiResponse.Articles = newsApiResponse.NewsApiResponse.Articles.Skip(skip).Take(pageSize).ToList();

            return newsApiResponse;
        }
    }
}
