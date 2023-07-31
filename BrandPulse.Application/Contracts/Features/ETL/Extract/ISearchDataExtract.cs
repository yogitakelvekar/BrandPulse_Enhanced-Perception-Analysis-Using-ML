using BrandPulse.Domain.Collections;

namespace BrandPulse.Application.Contracts.Features.ETL.Extract
{
    public interface ISearchDataExtract
    {
        Task<SocialMediaAggregates> ExtractAsync(string searchId);
    }
}