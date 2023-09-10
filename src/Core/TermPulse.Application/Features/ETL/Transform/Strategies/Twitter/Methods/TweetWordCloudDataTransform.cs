using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia.Tweeter;

namespace TermPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods
{
    public class TweetWordCloudDataTransform : IWordCloudDataTransform<Tweet>
    {
        public Task<IEnumerable<WordCloudTransformResult>> TransformAsync(IEnumerable<Tweet> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var result = data
                .Where(tweet => tweet?.entities?.hashtags?.Any() ?? false)
                .Select(tweet => TransformTweetToWordCloudResult(tweet, postDetails))
                .Where(result => result != null) // filter out any null results
                .Cast<WordCloudTransformResult>(); // cast items to non-nullable type

            return Task.FromResult(result.AsEnumerable());       
        }

        private WordCloudTransformResult? TransformTweetToWordCloudResult(Tweet tweet, IEnumerable<PostDetailTransformResult> postDetails)
        {
            if (tweet == null || tweet.entities == null || tweet.entities.hashtags == null)
            {
                return null;
            }

            var matchingPostDetail = postDetails.FirstOrDefault(pd => pd.PostId == tweet.id_str);

            if (matchingPostDetail == null)  // if no matching post detail is found, return null
            {
                return null;
            }

            var hashtags = tweet.entities.hashtags.Select(hashtag => hashtag.text).ToList();

            return new WordCloudTransformResult
            {
                PostDetailId = matchingPostDetail.Id,
                Hashtags = hashtags,
            };
        }
    }
}
