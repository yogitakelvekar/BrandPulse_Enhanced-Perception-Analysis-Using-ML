using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia.Youtube;
using Google.Apis.YouTube.v3.Data;
using System.Globalization;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Youtube.Methods
{
    public class YoutubeSentimentDataTransform : ISentimentDataTransform<YouTubeVideo>
    {
        public async Task<IEnumerable<SentimentTransformResult>> TransformAsync(IEnumerable<YouTubeVideo> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            
            IEnumerable<SentimentTransformResult> postResults = TransformVideoDetails(data, postDetails);
            IEnumerable<SentimentTransformResult> commentResults = TransformVideoComments(data, postDetails);
            IEnumerable<SentimentTransformResult> commentRepliesResults = TransformVideoCommentsReplies(data, postDetails);
            var result = postResults.Concat(commentResults).Concat(commentRepliesResults);
            return result;
        }

        private static IEnumerable<SentimentTransformResult> TransformVideoDetails(IEnumerable<YouTubeVideo> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var postResults = data
                .Where(video => video.Video != null)
                .Select(video => video.Video)
                .Select(post => new SentimentTransformResult
                {                
                    PostDetailId = postDetails.First(pd => pd.PostId == post.Id).Id,
                    PostContent = post.Snippet.Title,
                    PostLikes = (int)(post.Statistics?.LikeCount ?? 0),
                    PostDislikes = (int)(post.Statistics?.DislikeCount ?? 0),                  
                });
            return postResults;
        }

        private static IEnumerable<SentimentTransformResult> TransformVideoComments(IEnumerable<YouTubeVideo> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var commentResults = data
               .SelectMany(video => video.Comments ?? Enumerable.Empty<CommentThread>())
               .Where(thread => thread.Snippet?.TopLevelComment?.Snippet != null) 
               .Select(thread => new { TopLevelComment = thread.Snippet.TopLevelComment.Snippet, Id = thread.Snippet.TopLevelComment.Id })
               .Select(snippet => new SentimentTransformResult
               {
                   PostDetailId = postDetails.First(pd => pd.PostId == snippet.TopLevelComment.VideoId).Id,
                   SubPostId = snippet.Id,
                   PostContent = snippet.TopLevelComment.TextDisplay,
                   PostLikes = (int)(snippet.TopLevelComment.LikeCount ?? 0),
                   SubPostDate = DateTime.Parse(snippet.TopLevelComment.PublishedAtRaw, null, DateTimeStyles.AdjustToUniversal)
               });
            return commentResults;
        }

        private static IEnumerable<SentimentTransformResult> TransformVideoCommentsReplies(IEnumerable<YouTubeVideo> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var commentResults = data
                .SelectMany(video => video.Comments ?? Enumerable.Empty<CommentThread>())
                .SelectMany(thread => thread.Replies?.Comments ?? Enumerable.Empty<Comment>()) 
                .Select(comment => new SentimentTransformResult
                {
                    PostDetailId = postDetails.First(pd => pd.PostId == comment.Snippet.VideoId).Id,
                    SubPostId = comment.Id,
                    PostContent = comment.Snippet.TextDisplay,
                    PostLikes = (int)(comment.Snippet?.LikeCount ?? 0),
                    SubPostDate = DateTime.Parse(comment.Snippet.PublishedAtRaw, null, DateTimeStyles.AdjustToUniversal)
                });
            return commentResults;
        }
    }
}
