using TermPulse.API.Models.Response.Services;
using TermPulse.Application.Contracts.Infrastructure.HttpServices;
using TermPulse.Domain.SocialMedia.Reddit;
using TermPulse.Domain.SocialMedia.Youtube;
using Microsoft.AspNetCore.Mvc;

namespace TermPulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermPulseSocialMediaController : ControllerBase
    {
        private readonly IYouTubeHttpService youTubeHttpService;
        private readonly IRedditHttpService redditHttpService;
        private readonly ITwitterHttpService twitterHttpService;

        public TermPulseSocialMediaController(IYouTubeHttpService youTubeHttpService, IRedditHttpService redditHttpService, ITwitterHttpService twitterHttpService)
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
        public async Task<ActionResult<SocialMediaSearchResponse>> AggregateSearch(string term)
        {
            var twitterTask = twitterHttpService.SearchTweetsAsync(term);
            var youtubeTask = youTubeHttpService.SearchAndRetrieveVideoDataAsync(term);
            var redditTask = redditHttpService.SearchPosts(term);

            await Task.WhenAll(twitterTask, youtubeTask, redditTask);

            var aggregateData = new SocialMediaSearchResponse
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
