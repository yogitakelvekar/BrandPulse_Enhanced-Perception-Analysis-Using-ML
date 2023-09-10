using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia;
using TermPulse.Domain.SocialMedia.Reddit;
using TermPulse.Domain.SocialMedia.Tweeter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods
{
    public class TweetPostDataTransform : IPostDataTransform<Tweet>
    {
        public Task<IEnumerable<PostDetailTransformResult>> TransformAsync(IEnumerable<Tweet> data)
        {
            List<PostDetailTransformResult> tweetResults = new List<PostDetailTransformResult>();

            foreach (var tweet in data)
            {
                var result = TransformTweet(tweet);
                tweetResults.Add(result);
            }

            return Task.FromResult(tweetResults.AsEnumerable());
        }

        private PostDetailTransformResult TransformTweet(Tweet tweet)
        {
            // Check for null tweet
            if (tweet == null)
            {
                return new PostDetailTransformResult();
            }

            var mediaEntity = tweet.entities?.media?.FirstOrDefault();
            var user = tweet.user;

            var postId = tweet.id_str ?? string.Empty;
            var postTitle = $"{user?.name ?? string.Empty} | @{user?.screen_name ?? string.Empty}";
            var postDescription = tweet.full_text ?? string.Empty;
            var postUrl = $"https://twitter.com/{user?.screen_name}/status/{postId}";
            var postThumbnail = mediaEntity?.media_url_https ?? string.Empty;
            var publishDate = string.IsNullOrEmpty(tweet.created_at) ? DateTime.Now : ConvertTweetDateTime(tweet.created_at);
            var postAuthor = user?.name ?? string.Empty;
            var postAuthorAvatar = user?.profile_image_url_https ?? string.Empty;
            var postAuthorProfile = string.IsNullOrEmpty(user?.screen_name) ? string.Empty : $"https://twitter.com/{user.screen_name}";
            var location = user?.location ?? string.Empty;

            return new PostDetailTransformResult
            {
                PlatformId = (int)Platform.Twitter,
                PostId = postId,
                PostTitle = postTitle,
                PostDescription = postDescription,
                PostUrl = postUrl,
                PostThumbnail = postThumbnail,
                PublishDate = publishDate,
                PostAuthor = postAuthor,
                PostAuthorAvatar = postAuthorAvatar,
                PostAuthorProfile = postAuthorProfile,
                Location = location
            };
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
