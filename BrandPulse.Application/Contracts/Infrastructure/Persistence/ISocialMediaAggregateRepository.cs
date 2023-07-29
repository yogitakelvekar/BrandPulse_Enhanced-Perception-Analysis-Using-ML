using BrandPulse.Domain.Collections;

namespace BrandPulse.Application.Contracts.Infrastructure.Persistence
{
    public interface ISocialMediaAggregateRepository
    {
        Task<SocialMediaAggregates> GetDataAsync(string searchTermId);
        Task StoreDataAsync(SocialMediaAggregates data);
    }
}