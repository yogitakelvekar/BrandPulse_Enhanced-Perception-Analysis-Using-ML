using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Youtube.Methods
{
    public class YoutubeSentimentDataTransform : ISentimentDataTransform<YouTubeVideo>
    {
        public async Task<IEnumerable<SentimentTransformResult>> TransformAsync(IEnumerable<YouTubeVideo> data)
        {
            // Map RedditPost data
            IEnumerable<SentimentTransformResult> postResults = TransformVideoDetails(data);

            // Map RedditComment data
            IEnumerable<SentimentTransformResult> commentResults = TransformVideoComments(data);

            return postResults.Concat(commentResults);
        }

        private static IEnumerable<SentimentTransformResult> TransformVideoDetails(IEnumerable<YouTubeVideo> data)
        {
            var postResults = data
                .OfType<Video>()
                .Select(post => new SentimentTransformResult
                {
                    PostId = post.Id,
                    PlatformId = 3, // Change to your specific platform Id
                    PostContent = post.Snippet.Title,
                    PostLikes = (int)(post.Statistics?.LikeCount ?? 0),
                    PostDislikes = (int)(post.Statistics?.DislikeCount ?? 0),
                    PostDate = DateTime.Parse(post.Snippet.PublishedAtRaw)
                });
            return postResults;
        }

        private static IEnumerable<SentimentTransformResult> TransformVideoComments(IEnumerable<YouTubeVideo> data)
        {
            var commentResults = data
                .OfType<CommentThread>()
                .SelectMany(root => root.Replies.Comments)
                .Select(comment => new SentimentTransformResult
                {
                    PostId = comment.Id,
                    PlatformId = 3, // Change to your specific platform Id
                    PostContent = comment.Snippet.TextDisplay,
                    PostLikes = (int)(comment.Snippet?.LikeCount ?? 0),
                    PostDate = DateTime.Parse(comment.Snippet.PublishedAtRaw)
                });
            return commentResults;
        }
    }
}
