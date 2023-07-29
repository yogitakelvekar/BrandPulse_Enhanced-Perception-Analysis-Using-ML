using BrandPulse.Domain.Collections;

namespace BrandPulse.Application.Contracts.Features.ETL.Extract
{
    public interface ISocialMediaAggregate
    {
        Task<SocialMediaAggregates> SearchAndStore(string searchTerm);
    }
}