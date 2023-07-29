using BrandPulse.Application.Contracts.Infrastructure.HttpServices;
using BrandPulse.Domain.SocialMedia;
using BrandPulse.HttpServices.Settings;
using Microsoft.Extensions.Options;
using Reddit;
using Reddit.Inputs.Search;
using Post = Reddit.Controllers.Post;

namespace BrandPulse.HttpServices.Services
{
    public class RedditHttpService : IRedditHttpService
    {
        private readonly RedditClient _redditClient;
        private readonly HttpServicesSettings _appSettings;

        public RedditHttpService(IOptions<HttpServicesSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _redditClient = new RedditClient(
                appId: _appSettings.RedditSettings.AppId,
                refreshToken: _appSettings.RedditSettings.RefreshToken,
                accessToken: _appSettings.RedditSettings.AccessToken);
        }

        public async Task<IEnumerable<RedditPost>?> SearchPosts(string searchTerm)
        {
            try
            {
                // Get the search results
                var posts = _redditClient.Subreddit("all").Search(
                    new SearchGetSearchInput(q: searchTerm, limit: _appSettings.MaxResults, sort: "relevance"));

                var redditPosts = posts.Select(ToRedditPost);

                return await Task.WhenAll(redditPosts);
            }
            catch (AggregateException ex)
            {
                foreach (var innerEx in ex.InnerExceptions)
                {
                    // log innerEx.ToString() or handle accordingly
                }

                return null; // Or however you wish to handle this case
            }
            catch (Exception ex)
            {
                // Log exception
                // log ex.ToString() or handle accordingly
                return null; // Or however you wish to handle this case
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
    }
}
