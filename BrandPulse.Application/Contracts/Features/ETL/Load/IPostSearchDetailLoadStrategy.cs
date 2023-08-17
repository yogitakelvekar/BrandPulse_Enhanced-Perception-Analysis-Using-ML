using BrandPulse.Application.Models.ETL.Transform;

namespace BrandPulse.Application.Contracts.Features.ETL.Load
{
    public interface IPostSearchDetailLoadStrategy
    {
        Task<bool> LoadAsync(FinalTransformResult data);
    }
}