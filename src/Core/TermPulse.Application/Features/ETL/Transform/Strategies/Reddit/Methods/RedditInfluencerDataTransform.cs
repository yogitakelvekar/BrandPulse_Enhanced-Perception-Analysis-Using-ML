using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia.Reddit;

namespace TermPulse.Application.Features.ETL.Transform.Strategies.Reddit.Methods
{
    public class RedditInfluencerDataTransform : IInfluencerDataTransform<RedditPost>
    {
        public Task<IEnumerable<InfluencerTransformResult>> TransformAsync(IEnumerable<RedditPost> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var postResults = data
                .Where(post => post.GetType() == typeof(RedditPost))
                .Select(post => TransformRedditPostToInfluencerResult(post, postDetails));
            return Task.FromResult(postResults.AsEnumerable());
        }

        private InfluencerTransformResult TransformRedditPostToInfluencerResult(RedditPost post, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var potentialReach = post?.User?.PostKarma ?? 0;
            var engagement = (post?.UpVotes ?? 0) + (post?.DownVotes ?? 0) + (post?.Comments?.Count() ?? 0);
            var matchingPostDetail = postDetails.FirstOrDefault(pd => pd.PostId == post.Id);
            var postDetailId = matchingPostDetail?.Id ?? default(Guid);  // Assuming Id is of type Guid. Adjust as necessary.

            return new InfluencerTransformResult
            {
                PotentialReach = potentialReach,
                Engagement = engagement,
                PostDetailId = postDetailId,
            };
        }
    }
}
