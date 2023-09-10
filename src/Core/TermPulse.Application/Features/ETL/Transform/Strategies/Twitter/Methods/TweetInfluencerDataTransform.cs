using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia.Tweeter;

namespace TermPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods
{
    public class TweetInfluencerDataTransform : IInfluencerDataTransform<Tweet>
    {
        public Task<IEnumerable<InfluencerTransformResult>> TransformAsync(IEnumerable<Tweet> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var tweetResults = data
              .Select(tweet => TransformTweetToInfluencerResult(tweet, postDetails))
              .Where(result => result != null);
            return Task.FromResult(tweetResults.AsEnumerable());
        }

        private InfluencerTransformResult? TransformTweetToInfluencerResult(Tweet tweet, IEnumerable<PostDetailTransformResult> postDetails)
        {
            if (tweet == null || tweet.user == null)
            {
                return null;
            }

            var matchingPostDetail = postDetails.FirstOrDefault(pd => pd.PostId == tweet.id_str);

            if (matchingPostDetail == null)  // if no matching post detail is found, return null
            {
                return null;
            }

            var potentialReach = tweet?.user?.followers_count ?? 0;
            var engagement = (tweet?.retweet_count ?? 0) + (tweet?.favorite_count ?? 0);

            return new InfluencerTransformResult
            {
                PostDetailId = matchingPostDetail.Id,
                PotentialReach = potentialReach,
                Engagement = engagement,
            };
        }
    }
}
