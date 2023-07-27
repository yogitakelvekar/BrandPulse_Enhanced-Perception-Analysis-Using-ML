using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using BrandPulse.SocialMediaData.API.Models.Response.Services.BrandPulse.SocialMediaData.API.Models.Response.Services;
using BrandPulse.SocialMediaData.API.Models.Response.Services;

namespace BrandPulse.SocialMediaData.API.Models.Entities
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
