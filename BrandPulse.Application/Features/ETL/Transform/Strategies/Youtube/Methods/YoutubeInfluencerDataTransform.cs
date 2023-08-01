using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Youtube.Methods
{
    public class YoutubeInfluencerDataTransform : IInfluencerDataTransform<YouTubeVideo>
    {
        public Task<IEnumerable<InfluencerTransformResult>> TransformAsync(IEnumerable<YouTubeVideo> data)
        {
            var result = data.Select(video => new InfluencerTransformResult
            {
                AuthorName = video?.Video?.Snippet?.ChannelTitle ?? string.Empty,
                Avatar = video?.Channel?.Snippet?.Thumbnails?.Default__?.Url ?? string.Empty,
                // Profile =
                PotentialReach = Convert.ToInt32(video?.Channel?.Statistics?.SubscriberCount ?? 0),
                Engagement = Convert.ToInt32(video?.Video?.Statistics?.ViewCount ?? 0),
                Country = video?.Channel?.Snippet?.Country,
                PostId = video?.VideoId,
                PostDate = DateTime.Parse(video.Video.Snippet.PublishedAtRaw),
                PlatformId = 3
            });
            return Task.FromResult(result.AsEnumerable());
        }
    }
}
