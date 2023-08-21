using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using BrandPulse.Domain.SocialMedia.Reddit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Reddit.Methods
{
    public class RedditPostDataTransform : IPostDataTransform<RedditPost>
    {
        public Task<IEnumerable<PostDetailTransformResult>> TransformAsync(IEnumerable<RedditPost> data)
        {
            var postResults = data
                .Where(post => post.GetType() == typeof(RedditPost))
                .Select(post => new PostDetailTransformResult
                {
                    PlatformId = (int)Platform.Reddit,
                    PostId = post.Id,
                    PostTitle = post.Title.Substring(0, Math.Min(post.Title.Length, 100)),
                    PostDescription = post.Title,
                    PostUrl = $"https://www.reddit.com/r/{post.Subreddit}/comments/{post.Id}",
                    PostThumbnail = "https://www.redditstatic.com/icon.png",
                    PublishDate = post.Created,
                    PostAuthor = post.Author,
                    PostAuthorAvatar = post?.User?.Avatar ?? "https://www.redditstatic.com/avatars/avatar_default_02_FF4500.png",
                    PostAuthorProfile = post?.User?.ProfileUrl ?? $"https://www.reddit.com/user/{post.Author}",
                });
            return Task.FromResult(postResults.AsEnumerable());
        }
    }
}
