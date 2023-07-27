using BrandPulse.SocialMediaData.API.Models.Response.Services;
using BrandPulse.SocialMediaData.API.Services.FeatureServices;
using BrandPulse.SocialMediaData.API.Services.HttpServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrandPulse.SocialMediaData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandPulseController : ControllerBase
    {
        private readonly SocialMediaAggregateService aggregateService;

        public BrandPulseController(SocialMediaAggregateService aggregateService)
        {
            this.aggregateService = aggregateService;
        }

        [HttpGet("aggregate/{term}")]
        public async Task<ActionResult<SocialMediaAggregateResponse>> Aggregate(string term)
        {
            var result = await aggregateService.SearchAndStore(term);
            return Ok(result);
        }
    }
}
