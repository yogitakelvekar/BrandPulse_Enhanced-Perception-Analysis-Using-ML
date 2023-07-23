using BrandPulse.SocialMediaData.API.Models.Response.Services;
using BrandPulse.SocialMediaData.API.Services.HttpServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reddit.Controllers;

namespace BrandPulse.SocialMediaData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly YouTubeHttpService youTubeHttpService;
        private readonly RedditHttpService redditHttpService;
        private readonly TwitterHttpService twitterHttpService;

        public SocialMediaController(YouTubeHttpService youTubeHttpService, RedditHttpService redditHttpService, TwitterHttpService twitterHttpService)
        {
            this.youTubeHttpService = youTubeHttpService;
            this.redditHttpService = redditHttpService;
            this.twitterHttpService = twitterHttpService;
        }

        [HttpGet("youtube/search/{term}")]
        public async Task<ActionResult<YouTubeVideoData>> YouTubeSearch(string term)
        {
            var data = await youTubeHttpService.SearchAndRetrieveVideoDataAsync(term);
            return Ok(data);
        }

        [HttpGet("reddit/search/{term}")]
        public async Task<ActionResult<RedditPost>> RedditSearch(string term)
        {
            var data = redditHttpService.SearchPosts(term);
            return Ok(data);
        }

        [HttpGet("twitter/search/{term}")]
        public async Task<ActionResult<TweetResponse>> TweetSearch(string term)
        {
            var data = await twitterHttpService.SearchTweetsAsync(term);
            return Ok(data);
        }
    }
}
