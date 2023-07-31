using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using Microsoft.IdentityModel.Tokens;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods
{
    public class TweetWordCloudDataTransform : IWordCloudDataTransform<Tweet>
    {
        public Task<IEnumerable<WordCloudTransformResult>> TransformAsync(IEnumerable<Tweet> data)
        {
            var result = data.Where(tweet => !tweet.entities.hashtags.IsNullOrEmpty())
                .Select(tweet => new WordCloudTransformResult
                {
                    PostId = tweet.id_str,
                    PlatformId = 2, // Change to your specific platform Id
                    Hashtags = tweet.entities.hashtags.Select(hashtag => hashtag.text).ToList(),
                    PostDate = string.IsNullOrEmpty(tweet.created_at) ? DateTime.Now : Convert.ToDateTime(tweet.created_at)
                });
            return Task.FromResult(result.AsEnumerable());
        }
    }
}
