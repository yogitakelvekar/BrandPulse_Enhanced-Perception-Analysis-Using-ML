using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia.Reddit;

namespace TermPulse.Application.Features.ETL.Transform.Strategies.Reddit.Methods
{
    public class RedditSentimentDataTransform : ISentimentDataTransform<RedditPost>
    {
        public async Task<IEnumerable<SentimentTransformResult>> TransformAsync(IEnumerable<RedditPost> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            // Map RedditPost data
            IEnumerable<SentimentTransformResult> postResults = TransformPost(data, postDetails);

            // Map RedditComment data
            IEnumerable<SentimentTransformResult> commentResults = TransformComments(data, postDetails);

            return postResults.Concat(commentResults);
        }

        private static IEnumerable<SentimentTransformResult> TransformPost(IEnumerable<RedditPost> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var postResults = data
                .Where(post => post.GetType() == typeof(RedditPost))
                .Select(post => new SentimentTransformResult
                {                 
                    PostContent = post.Title,
                    PostDetailId = postDetails.First(pd => pd.PostId == post.Id).Id
                });
            return postResults;
        }

        private static IEnumerable<SentimentTransformResult> TransformComments(IEnumerable<RedditPost> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var commentResults = data
                .SelectMany(post => post.Comments?.Select(comment => new { PostId = post.Id, Comment = comment }) ?? Enumerable.Empty<dynamic>()) 
                .Select(item => new SentimentTransformResult
                {
                    PostContent = item.Comment.Body,
                    SubPostDate = item.Comment.Created,
                    PostDetailId = postDetails.First(pd => pd.PostId == item.PostId).Id 
                });
            return commentResults;
        }
    }
}
