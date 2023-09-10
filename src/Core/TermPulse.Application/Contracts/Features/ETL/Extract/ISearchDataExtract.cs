using TermPulse.Domain.Collections;

namespace TermPulse.Application.Contracts.Features.ETL.Extract
{
    public interface ISearchDataExtract
    {
        Task<SocialMediaAggregates> ExtractAsync(string searchId);
    }
}