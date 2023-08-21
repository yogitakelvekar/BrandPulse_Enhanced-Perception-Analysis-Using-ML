using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using BrandPulse.Domain.SocialMedia.Youtube;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Youtube.Methods
{
    public class YoutubePostDataTransform : IPostDataTransform<YouTubeVideo>
    {
        public Task<IEnumerable<PostDetailTransformResult>> TransformAsync(IEnumerable<YouTubeVideo> data)
        {
            List<PostDetailTransformResult> postResults = new List<PostDetailTransformResult>();

            foreach (var video in data)
            {
                var result = TransformYouTubeVideo(video);
                postResults.Add(result);
            }

            return Task.FromResult(postResults.AsEnumerable());
        }

        private PostDetailTransformResult TransformYouTubeVideo(YouTubeVideo video)
        {
            if (video == null)
            {
                return new PostDetailTransformResult();
            }

            var postId = video.VideoId ?? string.Empty;

            var snippet = video.Video?.Snippet;
            var postTitle = snippet?.Title ?? string.Empty;
            var postDescription = snippet?.Description ?? string.Empty;
            var postThumbnail = snippet?.Thumbnails?.Standard?.Url ?? string.Empty;
            var publishDateRaw = snippet?.PublishedAtRaw;
            var publishDate = !string.IsNullOrEmpty(publishDateRaw) ? DateTime.Parse(publishDateRaw) : DateTime.Now;
            var postAuthor = snippet?.ChannelTitle ?? string.Empty;

            var channelSnippet = video.Channel?.Snippet;
            var postAuthorAvatar = channelSnippet?.Thumbnails?.Default__?.Url ?? string.Empty;
            var channelId = video.Channel?.Id ?? string.Empty;
            var location = channelSnippet?.Country ?? string.Empty;

            var postUrl = $"https://www.youtube.com/watch?v={postId}";
            var postAuthorProfile = $"https://www.youtube.com/channel/{channelId}";

            return new PostDetailTransformResult
            {
                PlatformId = (int)Platform.Youtube,
                PostId = postId,
                PostTitle = postTitle,
                PostDescription = postDescription,
                PostUrl = postUrl,
                PostThumbnail = postThumbnail,
                PublishDate = publishDate,
                PostAuthor = postAuthor,
                PostAuthorAvatar = postAuthorAvatar,
                PostAuthorProfile = postAuthorProfile,
                Location = location
            };
        }

    }
}
