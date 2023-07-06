using BrandPulse.SocialMediaData.API.Models.Response;
using BrandPulse.SocialMediaData.API.Settings;
using Microsoft.Extensions.Options;
using Reddit;
using Reddit.Controllers;
using Reddit.Inputs.Search;
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

        public IEnumerable<Models.Response.PostData>? SearchPosts(string searchTerm, int maxResults = 25)
        {         
            // Get the search results
            var posts = _redditClient.Subreddit("all").Search(new SearchGetSearchInput(q: searchTerm,limit: maxResults));

            var postData = posts.Select(p => new Models.Response.PostData
            {
                Title = p.Title,
                Author = p.Author,
                Url = p.Permalink,
                Comments = p.Comments.GetComments().Select(c => new Models.Response.CommentData
                {
                    Author = c.Author,
                    Body = c.Body
                }).ToList()
            });

            return postData;
        }
    }
}
