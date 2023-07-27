using BrandPulse.SocialMediaData.API.Models.Response.Services;
using BrandPulse.SocialMediaData.API.Services.HttpServices;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<YouTubeVideo>> YouTubeSearch(string term)
        {
            var data = await youTubeHttpService.SearchAndRetrieveVideoDataAsync(term);
            return Ok(data);
        }

        [HttpGet("reddit/search/{term}")]
        public async Task<ActionResult<RedditPost>> RedditSearch(string term)
        {
            var data = await redditHttpService.SearchPosts(term);
            return Ok(data);
        }

        [HttpGet("twitter/search/{term}")]
        public async Task<IActionResult> TweetSearch(string term)
        {
            var data = await twitterHttpService.SearchTweetsAsync(term);
            return Ok(data);
        }

        [HttpGet("aggregate/search/{term}")]
        public async Task<ActionResult<SocialMediaAggregateResponse>> AggregateSearch(string term)
        {
            var twitterTask = twitterHttpService.SearchTweetsAsync(term);
            var youtubeTask = youTubeHttpService.SearchAndRetrieveVideoDataAsync(term);
            var redditTask = redditHttpService.SearchPosts(term);

            await Task.WhenAll(twitterTask, youtubeTask, redditTask);

            var aggregateData = new SocialMediaAggregateResponse
            {
                SearchTerm = term,
                Tweets = await twitterTask,
                YouTubeVideos = await youtubeTask,
                RedditPosts = await redditTask
            };

            return Ok(aggregateData);
        }
    }
}
