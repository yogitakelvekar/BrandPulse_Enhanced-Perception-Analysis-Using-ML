using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;


namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Reddit.Methods
{
    public class RedditSentimentDataTransform : ISentimentDataTransform<RedditPost>
    {
        public async Task<IEnumerable<SentimentTransformResult>> TransformAsync(IEnumerable<RedditPost> data)
        {
            // Map RedditPost data
            IEnumerable<SentimentTransformResult> postResults = TransformPost(data);

            // Map RedditComment data
            IEnumerable<SentimentTransformResult> commentResults = TransformComments(data);

            return postResults.Concat(commentResults);
        }

        private static IEnumerable<SentimentTransformResult> TransformPost(IEnumerable<RedditPost> data)
        {
            var postResults = data
                .Where(post => post.GetType() == typeof(RedditPost))
                .Select(post => new SentimentTransformResult
                {
                    PostId = post.Id,
                    PlatformId = 1, // Change to your specific platform Id
                    PostContent = post.Title,
                    PostDate = post.Created
                });
            return postResults;
        }

        private static IEnumerable<SentimentTransformResult> TransformComments(IEnumerable<RedditPost> data)
        {
            var commentResults = data
                .SelectMany(post => post.Comments ?? Enumerable.Empty<RedditComment>()) // Flatten the Comments collection
                .Select(comment => new SentimentTransformResult
                {
                    PostId = comment.Id,
                    PlatformId = 1, // Change to your specific platform Id
                    PostContent = comment.Body,
                    PostDate = comment.Created
                });
            return commentResults;
        }
    }
}
