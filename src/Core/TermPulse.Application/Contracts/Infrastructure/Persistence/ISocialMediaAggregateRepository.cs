using TermPulse.Domain.Collections;

namespace TermPulse.Application.Contracts.Infrastructure.Persistence
{
    public interface ISocialMediaAggregateRepository
    {
        Task<SocialMediaAggregates> GetDataAsync(string searchTermId);
        Task StoreDataAsync(SocialMediaAggregates data);
    }
}