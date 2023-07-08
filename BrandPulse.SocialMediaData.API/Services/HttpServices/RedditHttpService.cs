using BrandPulse.SocialMediaData.API.Models.Response;
using BrandPulse.SocialMediaData.API.Settings;
using Microsoft.Extensions.Options;
using Reddit;
using Reddit.Controllers;
using Reddit.Inputs.Search;
using Reddit.Models;
using Reddit.Things;

namespace BrandPulse.SocialMediaData.API.Services.HttpServices
{
    public class RedditHttpService
    {
        private readonly RedditClient _redditClient;
        private readonly ApplicationSettings _appSettings;
        public RedditHttpService(IOptions<ApplicationSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            _redditClient = new RedditClient(appId: _appSettings.RedditSettings.AppId, refreshToken: _appSettings.RedditSettings.RefreshToken, accessToken: _appSettings.RedditSettings.AccessToken);
        }

        public  IEnumerable<RedditPost>? SearchPosts(string searchTerm, int maxResults = 25)
        {
            // Get the search results
            var posts = _redditClient.Subreddit("all").Search(new SearchGetSearchInput(q: searchTerm,limit: maxResults, sort: "relevance"));

            var redditPosts = posts.Select(p => new RedditPost {
                Id = p.Id,
                Title = p.Title,
                Fullname = p.Fullname,
                Subreddit = p.Subreddit,
                Author = p.Author,
                DownVotes = p.DownVotes,
                UpVotes = p.UpVotes,
                UpvoteRatio = p.UpvoteRatio,
                Score = p.Score,
                Created = p.Created,
                Comments = p.Comments.GetComments(sort: "best", limit: 5, depth: 1).ToList()?.Select(c => new RedditComment
                {
                    Id = c.Id,
                    Body = c.Body,
                    Author = c.Author,
                    DownVotes = c.DownVotes,
                    UpVotes = c.UpVotes,
                    Score = c.Score,
                    //NumReplies = c.NumReplies,
                    Created = c.Created
                }) ?? Enumerable.Empty<RedditComment>()
            });
            return redditPosts;
        }
    }
}
