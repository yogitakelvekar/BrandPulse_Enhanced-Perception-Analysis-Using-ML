using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.Collections;

namespace TermPulse.Application.Contracts.Features.ETL.Transform
{
    public interface ISearchDataTransform
    {
        Task<FinalTransformResult> TransformAsync(SocialMediaAggregates data);
    }
}