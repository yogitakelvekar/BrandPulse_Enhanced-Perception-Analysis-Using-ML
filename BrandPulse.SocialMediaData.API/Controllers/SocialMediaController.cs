using BrandPulse.SocialMediaData.API.Models.Services;
using BrandPulse.SocialMediaData.API.Services.HttpServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrandPulse.SocialMediaData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly YouTubeHttpService youTubeHttpService;

        public SocialMediaController(YouTubeHttpService youTubeHttpService)
        {
            this.youTubeHttpService = youTubeHttpService;
        }

        [HttpGet("search/{term}")]
        public async Task<ActionResult<YouTubeVideoData>> Search(string term)
        {
           var data = await youTubeHttpService.SearchAndRetrieveVideoDataAsync(term);
           return Ok(data);
        }
    }
}
