using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.Collections;

namespace BrandPulse.Application.Contracts.Features.ETL.Transform
{
    public interface ISearchDataTransform
    {
        Task<FinalTransformResult> TransformAsync(SocialMediaAggregates data);
    }
}