using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods
{
    public class TweetSentimentDataTransform : ISentimentDataTransform<Tweet>
    {
        public async Task<IEnumerable<SentimentTransformResult>> TransformAsync(IEnumerable<Tweet> data)
        {
            var tweetResults = data
                .Select(tweet => new SentimentTransformResult
                {
                    PostId = tweet.id_str,
                    PlatformId = 2, // Change to your specific platform Id
                    PostContent = tweet.full_text,
                    PostDate = string.IsNullOrEmpty(tweet.created_at) ? DateTime.Now : Convert.ToDateTime(tweet.created_at)
                });
            return tweetResults;
        }
    }
}
