using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using BrandPulse.Domain.SocialMedia.Tweeter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods
{
    public class TweetInfluencerDataTransform : IInfluencerDataTransform<Tweet>
    {
        public Task<IEnumerable<InfluencerTransformResult>> TransformAsync(IEnumerable<Tweet> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var tweetResults = data
              .Select(tweet => new InfluencerTransformResult
              {
                  PostDetailId = postDetails.First(pd => pd.PostId == tweet.id_str).Id,
                  PotentialReach = tweet.user.followers_count,
                  Engagement = tweet.retweet_count + tweet.favorite_count,                                         
              });
            return Task.FromResult(tweetResults.AsEnumerable());
        }
    }
}
