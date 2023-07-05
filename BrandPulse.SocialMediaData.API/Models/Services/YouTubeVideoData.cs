using Google.Apis.YouTube.v3.Data;

namespace BrandPulse.SocialMediaData.API.Models.Services
{
    public class YouTubeVideoData
    {
        public SearchResult? SearchItem { get; set; }
        public Video? VideoDetails { get; set; }
        public List<CommentThread>? Comments { get; set; }
    }
}
