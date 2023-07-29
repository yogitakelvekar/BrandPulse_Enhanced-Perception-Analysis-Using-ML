using BrandPulse.API.Models.Response.Services;
using BrandPulse.Application.Contracts.Infrastructure.HttpServices;
using BrandPulse.Domain.SocialMedia;
using Microsoft.AspNetCore.Mvc;

namespace BrandPulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly IYouTubeHttpService youTubeHttpService;
        private readonly IRedditHttpService redditHttpService;
        private readonly ITwitterHttpService twitterHttpService;

        public SocialMediaController(IYouTubeHttpService youTubeHttpService, IRedditHttpService redditHttpService, ITwitterHttpService twitterHttpService)
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
