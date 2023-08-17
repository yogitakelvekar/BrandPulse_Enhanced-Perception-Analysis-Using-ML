using BrandPulse.Domain.SocialMedia.Reddit;
using BrandPulse.Domain.SocialMedia.Tweeter;
using BrandPulse.Domain.SocialMedia.Youtube;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BrandPulse.Domain.Collections
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
