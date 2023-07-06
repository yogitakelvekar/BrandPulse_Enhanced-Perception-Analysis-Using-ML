using BrandPulse.SocialMediaData.API.Models.Response;
using BrandPulse.SocialMediaData.API.Models.Services;
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

        public SocialMediaController(YouTubeHttpService youTubeHttpService, RedditHttpService redditHttpService)
        {
            this.youTubeHttpService = youTubeHttpService;
            this.redditHttpService = redditHttpService;
        }

        [HttpGet("search/{term}")]
        public async Task<ActionResult<PostData>> Search(string term)
        {
            // var data = await youTubeHttpService.SearchAndRetrieveVideoDataAsync(term);
            var data = redditHttpService.SearchPosts(term);
            return Ok(data);
        }
    }
}
