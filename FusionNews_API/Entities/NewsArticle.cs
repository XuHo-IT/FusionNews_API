using Newtonsoft.Json;

namespace FusionNews_API.Entities
{
    public class NewsArticle
    {
        [JsonProperty("article_id")]
        public string ArticleId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }

        [JsonProperty("creator")]
        public List<string> Creator { get; set; }
        [JsonIgnore]
        public string CreatorDisplay => Creator?.FirstOrDefault() ?? "Unknown";

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("pubDate")]
        public string PubDate { get; set; }

        [JsonProperty("pubDateTZ")]
        public string PubDateTZ { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }

        [JsonProperty("source_id")]
        public string SourceId { get; set; }

        [JsonProperty("source_name")]
        public string SourceName { get; set; }

        [JsonProperty("source_priority")]
        public int SourcePriority { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        [JsonProperty("source_icon")]
        public string SourceIcon { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("country")]
        public List<string> Country { get; set; }

        [JsonProperty("category")]
        public List<string> Category { get; set; }

        [JsonProperty("duplicate")]
        public bool Duplicate { get; set; }
    }

    public class NewsApiResponse
    {
        [JsonProperty("results")]
        public List<NewsArticle> Results { get; set; }
    }

}
