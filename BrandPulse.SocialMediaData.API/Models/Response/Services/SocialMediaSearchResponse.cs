using BrandPulse.Domain.SocialMedia;

namespace BrandPulse.API.Models.Response.Services
{
    public class SocialMediaSearchResponse
    {
        public Guid SearchId { get; private set; }
        public DateTime SearchTimestamp { get; private set; }
        public string SearchTerm { get; set; }
        public IEnumerable<Tweet>? Tweets { get; set; }
        public IEnumerable<YouTubeVideo>? YouTubeVideos { get; set; }
        public IEnumerable<RedditPost>? RedditPosts { get; set; }

        public SocialMediaSearchResponse()
        {
            SearchId = Guid.NewGuid();
            SearchTimestamp = DateTime.UtcNow;
        }
    }
}
