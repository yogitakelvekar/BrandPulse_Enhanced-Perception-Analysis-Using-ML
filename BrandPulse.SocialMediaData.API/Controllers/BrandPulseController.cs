using BrandPulse.API.Models.Response.Services;
using BrandPulse.Application.Contracts.Features.DataSearch;
using Microsoft.AspNetCore.Mvc;

namespace BrandPulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandPulseController : ControllerBase
    {
        private readonly ISocialMediaSearch socialMediaSearch;

        public BrandPulseController(ISocialMediaSearch socialMediaSearch)
        {
            this.socialMediaSearch = socialMediaSearch;
        }

        [HttpGet("search/{term}")]
        public async Task<ActionResult<SocialMediaSearchResponse>> Search(string term)
        {
            var result = await socialMediaSearch.SearchAllAndStore(term);
            return Ok(result);
        }
    }
}
