using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using BrandPulse.Domain.SocialMedia.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Youtube.Methods
{
    public class YoutubePostDataTransform : IPostDataTransform<YouTubeVideo>
    {
        public Task<IEnumerable<PostDetailTransformResult>> TransformAsync(IEnumerable<YouTubeVideo> data)
        {
            var result = data.Select(video => new PostDetailTransformResult
            {
                PlatformId = (int)Platform.Youtube,
                PostId = video?.VideoId ?? string.Empty,
                PostTitle = video?.Video?.Snippet?.Title ?? string.Empty,
                PostDescription = video?.Video?.Snippet?.Description ?? string.Empty,
                PostUrl = $"https://www.youtube.com/watch?v={video?.VideoId}",
                PostThumbnail = video?.Video?.Snippet?.Thumbnails.Standard.Url ?? string.Empty,
                PublishDate = DateTime.Parse(video.Video.Snippet.PublishedAtRaw),
                PostAuthor = video?.Video?.Snippet?.ChannelTitle ?? string.Empty,
                PostAuthorAvatar = video?.Channel?.Snippet?.Thumbnails?.Default__?.Url ?? string.Empty,
                PostAuthorProfile = $"https://www.youtube.com/channel/{video?.Channel?.Id ?? string.Empty}",
                Location = video?.Channel?.Snippet?.Country ?? string.Empty      
            });
            return Task.FromResult(result.AsEnumerable());
        }
    }
}
