using BrandPulse.Domain.SocialMedia;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BrandPulse.Domain.Collections
{
    public class SocialMediaAggregates
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<Tweet>? Tweets { get; set; }
        public IEnumerable<YouTubeVideo>? YouTubeVideos { get; set; }
        public IEnumerable<RedditPost>? RedditPosts { get; set; }
    }
}
