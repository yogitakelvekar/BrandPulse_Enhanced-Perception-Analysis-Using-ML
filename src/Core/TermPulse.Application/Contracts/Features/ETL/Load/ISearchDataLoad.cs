using TermPulse.Application.Models.ETL.Transform;

namespace TermPulse.Application.Contracts.Features.ETL.Load
{
    public interface ISearchDataLoad
    {
        Task<bool> LoadAsync(FinalTransformResult transformResult);
    }
}