using BrandPulse.API.Models.Response.Services;
using BrandPulse.Application.Contracts.Features.ETL.Extract;
using Microsoft.AspNetCore.Mvc;

namespace BrandPulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandPulseController : ControllerBase
    {
        private readonly ISocialMediaAggregate aggregateService;

        public BrandPulseController(ISocialMediaAggregate aggregateService)
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
