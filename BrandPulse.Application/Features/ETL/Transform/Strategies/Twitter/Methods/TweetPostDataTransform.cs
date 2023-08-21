using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using BrandPulse.Domain.SocialMedia.Reddit;
using BrandPulse.Domain.SocialMedia.Tweeter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods
{
    public class TweetPostDataTransform : IPostDataTransform<Tweet>
    {
        public Task<IEnumerable<PostDetailTransformResult>> TransformAsync(IEnumerable<Tweet> data)
        {
            var tweetResults = data
               .Select(tweet => new PostDetailTransformResult
               {
                   PlatformId = (int)Platform.Twitter,
                   PostId = tweet.id_str,
                   PostTitle = $"{tweet.user.name} | @{tweet.user.screen_name}",
                   PostDescription = tweet.full_text,
                   PostUrl = tweet.entities.media.FirstOrDefault()?.url ?? string.Empty,
                   PostThumbnail = tweet.entities.media.FirstOrDefault()?.media_url_https ?? string.Empty,
                   PublishDate = string.IsNullOrEmpty(tweet.created_at) ? DateTime.Now : ConvertTweetDateTime(tweet.created_at),
                   PostAuthor = tweet.user.name,
                   PostAuthorAvatar = tweet.user.profile_image_url_https,
                   PostAuthorProfile = $"https://twitter.com/{tweet.user.screen_name}",
                   Location = tweet.user.location
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
