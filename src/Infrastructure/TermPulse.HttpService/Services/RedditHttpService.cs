using TermPulse.Application.Contracts.Infrastructure.HttpServices;
using TermPulse.Domain.SocialMedia.Reddit;
using TermPulse.HttpServices.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reddit;
using Reddit.Inputs.Search;
using Post = Reddit.Controllers.Post;

namespace TermPulse.HttpServices.Services
{
    public class RedditHttpService : IRedditHttpService
    {
        private readonly RedditClient _redditClient;
        private readonly HttpServicesSettings _appSettings;
        private readonly ILogger<RedditHttpService> logger;

        public RedditHttpService(IOptions<HttpServicesSettings> appSettings, ILogger<RedditHttpService> logger)
        {
            _appSettings = appSettings.Value;
            _redditClient = new RedditClient(
                appId: _appSettings.RedditSettings.AppId,
                refreshToken: _appSettings.RedditSettings.RefreshToken,
                accessToken: _appSettings.RedditSettings.AccessToken);
            this.logger = logger;
        }

        public async Task<IEnumerable<RedditPost>?> SearchPosts(string searchTerm)
        {
            try
            {
                var posts = _redditClient.Subreddit("all").Search(
                    new SearchGetSearchInput(q: searchTerm, limit: _appSettings.MaxResults, sort: "relevance", t: "month"));
                var redditPosts = posts.Select(ToRedditPost);
                var result = await Task.WhenAll(redditPosts);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace);
                return null;
            }
        }

        private async Task<RedditPost> ToRedditPost(Post post)
        {
            var redditPost = new RedditPost
            {
                Id = post.Id,
                Title = post.Title,
                Fullname = post.Fullname,
                Subreddit = post.Subreddit,
                Author = post.Author,
                DownVotes = post.DownVotes,
                UpVotes = post.UpVotes,
                UpvoteRatio = post.UpvoteRatio,
                Score = post.Score,
                Created = post.Created,
                User = GetUserData(post),
                Comments = new List<RedditComment>()
            };

            await AddCommentsToRedditPost(post, redditPost);

            return redditPost;
        }

        private Task AddCommentsToRedditPost(Post post, RedditPost redditPost)
        {
            return Task.Run(() =>
            {
                var comments = post.Comments.GetComments(sort: "best", limit: _appSettings.MaxComments, depth: 1);
                redditPost.Comments = comments.Select(c => new RedditComment
                {
                    Id = c.Id,
                    Body = c.Body,
                    Author = c.Author,
                    DownVotes = c.DownVotes,
                    UpVotes = c.UpVotes,
                    Score = c.Score,
                    //NumReplies = c.NumReplies,
                    Created = c.Created
                }).ToList();
            });
        }

        public RedditUser? GetUserData(Post post)
        {
            RedditUser? user = null;
            try
            {
                // Get the search results
                var redditUserDetail = _redditClient.SearchUsers(
                    new SearchGetSearchInput(q: post.Listing.Author, limit: _appSettings.MaxResults, sort: "relevance"));
                var userData = redditUserDetail?.FirstOrDefault() ?? null;
                if (userData != null)
                {
                    user = new RedditUser();
                    user.UserId = userData.Id;
                    user.UserName = userData.Name;
                    user.PostKarma = userData.LinkKarma;
                    user.CommentKarma = userData.CommentKarma;
                    user.Avatar = userData.IconImg;
                    user.ProfileUrl = $"https://www.reddit.com/user/{userData.Name}";
                }
                
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace);
            }
            return user;
        }
    }
}
