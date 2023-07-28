using Google.Apis.YouTube.v3.Data;

namespace BrandPulse.Domain.SocialMedia
{
    public class YouTubeVideo
    {
        public string? VideoId { get; set; }
        public Video? Video { get; set; }
        public Channel? Channel { get; set; }
        public List<CommentThread>? Comments { get; set; }
    }
}
