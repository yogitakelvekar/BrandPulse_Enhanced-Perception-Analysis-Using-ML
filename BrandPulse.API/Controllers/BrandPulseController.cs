using BrandPulse.API.Models.Response.Services;
using BrandPulse.Application.Contracts.Features.DataSearch;
using BrandPulse.Application.Contracts.Infrastructure.MessagingBus;
using BrandPulse.Application.Models.Infrastructure.MessagingBus;
using Microsoft.AspNetCore.Mvc;

namespace BrandPulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandPulseController : ControllerBase
    {
        private readonly ISocialMediaSearch socialMediaSearch;
        private readonly IQueueMessagingBus<ETLMessage> messageBus;

        public BrandPulseController(ISocialMediaSearch socialMediaSearch, IQueueMessagingBus<ETLMessage> messageBus)
        {
            this.socialMediaSearch = socialMediaSearch;
            this.messageBus = messageBus;
        }

        [HttpGet("search/{term}")]
        public async Task<ActionResult<SocialMediaSearchResponse>> Search(string term)
        {
            var result = await socialMediaSearch.SearchAllAndStore(term);
            if (result.Id != null)
            {
                messageBus.SendMessageAsync(new ETLMessage { SearchTermId = result.Id });
            }
            return Ok(result);
        }
    }
}
