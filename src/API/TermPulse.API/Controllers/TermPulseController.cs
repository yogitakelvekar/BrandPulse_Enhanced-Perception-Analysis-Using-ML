using TermPulse.API.Models.Response.Services;
using TermPulse.Application.Contracts.Features.DataSearch;
using TermPulse.Application.Contracts.Infrastructure.MessagingBus;
using TermPulse.Application.Models.Infrastructure.MessagingBus;
using Microsoft.AspNetCore.Mvc;

namespace TermPulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermPulseController : ControllerBase
    {
        private readonly ISocialMediaSearch socialMediaSearch;
        private readonly IQueueMessagingBus<ETLMessage> messageBus;

        public TermPulseController(ISocialMediaSearch socialMediaSearch, IQueueMessagingBus<ETLMessage> messageBus)
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
                await messageBus.SendMessageAsync(new ETLMessage { SearchTermId = result.Id });
            }
            return Ok(result);
        }
    }
}
