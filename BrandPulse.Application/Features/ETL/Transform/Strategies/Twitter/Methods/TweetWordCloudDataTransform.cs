using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia.Tweeter;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods
{
    public class TweetWordCloudDataTransform : IWordCloudDataTransform<Tweet>
    {
        public Task<IEnumerable<WordCloudTransformResult>> TransformAsync(IEnumerable<Tweet> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var result = data.Where(tweet => tweet.entities.hashtags.Any())
               .Select(tweet => new WordCloudTransformResult
               {              
                   PostDetailId = postDetails.First(pd => pd.PostId == tweet.id_str).Id,
                   Hashtags = tweet.entities.hashtags.Select(hashtag => hashtag.text).ToList(),                
               });
            return Task.FromResult(result.AsEnumerable());
        }
    }
}
