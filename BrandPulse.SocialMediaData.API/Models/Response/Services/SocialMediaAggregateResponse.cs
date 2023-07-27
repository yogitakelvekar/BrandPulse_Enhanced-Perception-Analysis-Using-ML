using System;
using System.Collections.Generic;
using System.Text.Json;
using BrandPulse.SocialMediaData.API.Models.Response.Services;
using BrandPulse.SocialMediaData.API.Models.Response.Services.BrandPulse.SocialMediaData.API.Models.Response.Services;

namespace BrandPulse.SocialMediaData.API.Models.Response.Services
{
    public class SocialMediaAggregateResponse
    {
        public Guid SearchId { get; private set; }
        public DateTime SearchTimestamp { get; private set; }
        public string SearchTerm { get; set; }
        public IEnumerable<Tweet>? Tweets { get; set; }
        public IEnumerable<YouTubeVideo>? YouTubeVideos { get; set; }
        public IEnumerable<RedditPost>? RedditPosts { get; set; }

        public SocialMediaAggregateResponse()
        {
            // Assign a new GUID and the current timestamp whenever a new instance is created
            SearchId = Guid.NewGuid();
            SearchTimestamp = DateTime.UtcNow;
        }
    }
}
