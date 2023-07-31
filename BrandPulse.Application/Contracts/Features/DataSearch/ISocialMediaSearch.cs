using BrandPulse.Domain.Collections;

namespace BrandPulse.Application.Contracts.Features.DataSearch
{
    public interface ISocialMediaSearch
    {
        Task<SocialMediaAggregates> SearchAllAndStore(string searchTerm);
    }
}