using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
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
        public Task<IEnumerable<InfluencerTransformResult>> TransformAsync(IEnumerable<Tweet> data)
        {
            var tweetResults = data
               .Select(tweet => new InfluencerTransformResult
               {
                   AuthorName = tweet.user.name,
                   Avatar = tweet.user.profile_image_url_https,
                   PotentialReach = tweet.user.followers_count,
                   Engagement = tweet.retweet_count + tweet.favorite_count,
                   Profile = tweet.user.url,
                   Country = tweet.user.location,
                   PostId = tweet.id_str,
                   PlatformId = 2, // Change to your specific platform Id
                   PostDate = string.IsNullOrEmpty(tweet.created_at) ? DateTime.Now : ConvertTweetDateTime(tweet.created_at)
               });
            return Task.FromResult(tweetResults.AsEnumerable());
        }

        private DateTime ConvertTweetDateTime(string datetime)
        {
            string dateStr = datetime;
            string format = "ddd MMM dd HH:mm:ss +0000 yyyy";
            DateTime dateTime = DateTime.ParseExact(dateStr, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            return dateTime;
        }
    }
}
