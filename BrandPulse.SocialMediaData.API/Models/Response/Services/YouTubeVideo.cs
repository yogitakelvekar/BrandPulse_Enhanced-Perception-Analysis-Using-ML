using Google.Apis.YouTube.v3.Data;

namespace BrandPulse.SocialMediaData.API.Models.Response.Services
{
    public class YouTubeVideo
    {
        public string? VideoId { get; set; }
        public Video? Video { get; set; }
        public Channel? Channel { get; set; }
        public List<CommentThread>? Comments { get; set; }
    }
}
