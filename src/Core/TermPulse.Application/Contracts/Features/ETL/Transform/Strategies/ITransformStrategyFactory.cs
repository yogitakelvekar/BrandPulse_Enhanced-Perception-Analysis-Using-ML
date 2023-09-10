using TermPulse.Domain.Collections;

namespace TermPulse.Application.Contracts.Features.ETL.Transform.Strategies
{
    public interface ITransformStrategyFactory
    {
        IEnumerable<ITransformStrategy> GetStrategies(SocialMediaAggregates data);
    }
}