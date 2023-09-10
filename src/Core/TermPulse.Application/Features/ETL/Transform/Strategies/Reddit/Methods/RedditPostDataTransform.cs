using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia;
using TermPulse.Domain.SocialMedia.Reddit;

namespace TermPulse.Application.Features.ETL.Transform.Strategies.Reddit.Methods
{
    public class RedditPostDataTransform : IPostDataTransform<RedditPost>
    {
        public Task<IEnumerable<PostDetailTransformResult>> TransformAsync(IEnumerable<RedditPost> data)
        {
            List<PostDetailTransformResult> postResults = new List<PostDetailTransformResult>();

            foreach (var post in data)
            {
                if (post is RedditPost)
                {
                    var result = TransformRedditPost(post);
                    postResults.Add(result);
                }
            }

            return Task.FromResult(postResults.AsEnumerable());
        }

        private PostDetailTransformResult TransformRedditPost(RedditPost post)
        {
            if (post == null)
            {
                return new PostDetailTransformResult();
            }

            var postId = post.Id ?? string.Empty;

            var postTitle = string.Empty;
            if (!string.IsNullOrEmpty(post.Title))
            {
                postTitle = post.Title.Substring(0, Math.Min(post.Title.Length, 100));
            }

            var postDescription = post.Title ?? string.Empty;

            var postSubreddit = post.Subreddit ?? string.Empty;
            var postUrl = $"https://www.reddit.com/r/{postSubreddit}/comments/{postId}";

            var publishDate = post.Created;

            var postAuthor = post.Author ?? string.Empty;
            var defaultAvatar = "https://www.redditstatic.com/avatars/avatar_default_02_FF4500.png";
            var postAuthorAvatar = post?.User?.Avatar ?? defaultAvatar;
            var postAuthorProfile = post?.User?.ProfileUrl ?? $"https://www.reddit.com/user/{postAuthor}";

            return new PostDetailTransformResult
            {
                PlatformId = (int)Platform.Reddit,
                PostId = postId,
                PostTitle = postTitle,
                PostDescription = postDescription,
                PostUrl = postUrl,
                PostThumbnail = "https://www.redditstatic.com/icon.png",
                PublishDate = publishDate,
                PostAuthor = postAuthor,
                PostAuthorAvatar = postAuthorAvatar,
                PostAuthorProfile = postAuthorProfile,
            };
        }
    }
}
