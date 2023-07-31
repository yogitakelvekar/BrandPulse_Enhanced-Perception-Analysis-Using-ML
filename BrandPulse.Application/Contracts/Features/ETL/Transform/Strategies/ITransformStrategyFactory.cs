using BrandPulse.Domain.Collections;

namespace BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies
{
    public interface ITransformStrategyFactory
    {
        IEnumerable<ITransformStrategy> GetStrategies(SocialMediaAggregates data);
    }
}