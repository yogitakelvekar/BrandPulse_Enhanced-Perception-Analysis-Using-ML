using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Youtube.Methods
{
    public class YoutubeSentimentDataTransform : ISentimentDataTransform<YouTubeVideo>
    {
        public async Task<IEnumerable<SentimentTransformResult>> TransformAsync(IEnumerable<YouTubeVideo> data)
        {
            
            IEnumerable<SentimentTransformResult> postResults = TransformVideoDetails(data);
            IEnumerable<SentimentTransformResult> commentResults = TransformVideoComments(data);
            IEnumerable<SentimentTransformResult> commentRepliesResults = TransformVideoCommentsReplies(data);
            var result = postResults.Concat(commentResults).Concat(commentRepliesResults);
            return result;
        }

        private static IEnumerable<SentimentTransformResult> TransformVideoDetails(IEnumerable<YouTubeVideo> data)
        {
            var postResults = data
                .Where(video => video.Video != null) // Ensure that the Video property is not null
                .Select(video => video.Video) // Select the Video property
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
               .SelectMany(video => video.Comments ?? Enumerable.Empty<CommentThread>())
               .Where(thread => thread.Snippet?.TopLevelComment?.Snippet != null) // Make sure the TopLevelComment exists
               .Select(thread => thread.Snippet.TopLevelComment.Snippet)
               .Select(snippet => new SentimentTransformResult
               {
                   PostId = snippet.VideoId,
                   PlatformId = 3, // Change to your specific platform Id
                   PostContent = snippet.TextDisplay,
                   PostLikes = (int)(snippet.LikeCount ?? 0),
                   PostDate = DateTime.Parse(snippet.PublishedAtRaw, null, DateTimeStyles.AdjustToUniversal)
               });
            return commentResults;
        }

        private static IEnumerable<SentimentTransformResult> TransformVideoCommentsReplies(IEnumerable<YouTubeVideo> data)
        {
            var commentResults = data
                .SelectMany(video => video.Comments ?? Enumerable.Empty<CommentThread>()) // Handles null Comments
                .SelectMany(thread => thread.Replies?.Comments ?? Enumerable.Empty<Comment>()) // Handles null Replies
                .Select(comment => new SentimentTransformResult
                {
                    PostId = comment.Id,
                    PlatformId = 3, // Change to your specific platform Id
                    PostContent = comment.Snippet.TextDisplay,
                    PostLikes = (int)(comment.Snippet?.LikeCount ?? 0),
                    PostDate = DateTime.Parse(comment.Snippet.PublishedAtRaw, null, DateTimeStyles.AdjustToUniversal)
                });
            return commentResults;
        }
    }
}
