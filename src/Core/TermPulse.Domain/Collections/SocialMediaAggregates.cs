using TermPulse.Domain.SocialMedia.Reddit;
using TermPulse.Domain.SocialMedia.Tweeter;
using TermPulse.Domain.SocialMedia.Youtube;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TermPulse.Domain.Collections
{
    public class SocialMediaAggregates
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string SearchTerm { get; set; } = string.Empty;
        public DateTime SearchDateTime { get; set; } = DateTime.Now;
        public IEnumerable<Tweet>? Tweets { get; set; }
        public IEnumerable<YouTubeVideo>? YouTubeVideos { get; set; }
        public IEnumerable<RedditPost>? RedditPosts { get; set; }
    }
}
