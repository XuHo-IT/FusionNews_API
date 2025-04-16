using Application.Entities.Base;

namespace FusionNews_API.Helpers
{
    public class Util
    {
        public static List<NewsArticle> Filtering(List<NewsArticle> articles, string? filterOn = null, string? filterRequest = null)
        {
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterRequest))
            {
                if (filterOn.Equals("Country", StringComparison.OrdinalIgnoreCase))
                {
                    articles = articles.Where(a => a.Country.Any(c => c.ToUpper().Contains(filterRequest.ToUpper()))).ToList();
                }
                if (filterOn.Equals("Category", StringComparison.OrdinalIgnoreCase))
                {
                    articles = articles.Where(a => a.Category.Any(c => c.ToUpper().ToUpper().Contains(filterRequest.ToUpper()))).ToList();
                }
                if(filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    articles = articles.Where(a => a.Title.ToUpper().Contains(filterRequest.ToUpper())).ToList();
                }
            }

            return articles;
        }

        public static List<NewsArticle> Pagination(int pageNumber,int pageSize, List<NewsArticle> articles)
        {
            var skip = (pageNumber - 1) * pageSize;
            articles = articles.Skip(skip).Take(pageSize).ToList();

            return articles;
        }
    }
}
