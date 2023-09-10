using TermPulse.Application.Models.ETL.Transform;

namespace TermPulse.Application.Contracts.Features.ETL.Load
{
    public interface IPostSearchDetailLoadStrategy
    {
        Task<bool> LoadAsync(FinalTransformResult data);
    }
}