using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia.Tweeter;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods
{
    public class TweetSentimentDataTransform : ISentimentDataTransform<Tweet>
    {
        public async Task<IEnumerable<SentimentTransformResult>> TransformAsync(IEnumerable<Tweet> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var tweetResults = data
                .Select(tweet => new SentimentTransformResult
                {
                    PostDetailId = postDetails.First(pd => pd.PostId == tweet.id_str).Id,
                    PostContent = tweet.full_text,
                });
            //var tweetResults = new List<SentimentTransformResult>();
            //foreach (var tweet in data)
            //{
            //    var x = new SentimentTransformResult();
            //    x.PostDetailId = postDetails.First(pd => pd.PostId == tweet.id_str).Id;
            //    x.PostContent = tweet.full_text;
            //    tweetResults.Add(x);
            //}
            return tweetResults;
        }
    }
}
