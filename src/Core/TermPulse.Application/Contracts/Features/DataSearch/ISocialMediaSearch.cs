using TermPulse.Domain.Collections;

namespace TermPulse.Application.Contracts.Features.DataSearch
{
    public interface ISocialMediaSearch
    {
        Task<SocialMediaAggregates> SearchAllAndStore(string searchTerm);
    }
}