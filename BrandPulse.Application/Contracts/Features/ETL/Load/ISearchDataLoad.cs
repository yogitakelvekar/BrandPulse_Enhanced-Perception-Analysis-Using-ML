using BrandPulse.Application.Models.ETL.Transform;

namespace BrandPulse.Application.Contracts.Features.ETL.Load
{
    public interface ISearchDataLoad
    {
        Task<bool> LoadAsync(FinalTransformResult transformResult);
    }
}