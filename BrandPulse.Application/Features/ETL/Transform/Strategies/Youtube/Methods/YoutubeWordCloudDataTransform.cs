using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia.Youtube;
using Google.Apis.YouTube.v3.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Youtube.Methods
{
    public class YoutubeWordCloudDataTransform : IWordCloudDataTransform<YouTubeVideo>
    {
        public Task<IEnumerable<WordCloudTransformResult>> TransformAsync(IEnumerable<YouTubeVideo> data)
        {
            var postResults = data
             .Select(v => v.Video)
             .Where(video => video.Snippet.Tags != null && video.Snippet.Tags.Any())
             .Select(post => new WordCloudTransformResult
             {
                 PostId = post.Id,
                 PlatformId = 3, // Change to your specific platform Id
                 Hashtags = post.Snippet.Tags.ToList(),
                 PostDate = DateTime.Parse(post.Snippet.PublishedAtRaw, null, DateTimeStyles.RoundtripKind)
             });
            return Task.FromResult(postResults.AsEnumerable());
        }
    }
}
