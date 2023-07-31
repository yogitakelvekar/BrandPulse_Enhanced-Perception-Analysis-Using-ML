using BrandPulse.Application.Contracts.Features.ETL.Transform;
using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform
{
    public class SearchDataTransform : ISearchDataTransform
    {
        private readonly ITransformStrategyFactory transformStrategyFactory;

        public SearchDataTransform(ITransformStrategyFactory transformStrategyFactory)
        {
            this.transformStrategyFactory = transformStrategyFactory;
        }

        public async Task<TransformResult> TransformAsync(SocialMediaAggregates data)
        {
            TransformResult transformResult = new TransformResult();
            var transformStrategies = transformStrategyFactory.GetStrategies(data);
            foreach (var strategy in transformStrategies)
            {
                var result = await strategy.TransformAsync();
                transformResult.AddSentimentTransformResult(result.SentimentTransformResult);
                transformResult.AddWordCloudTransformResult(result.WordCloudTransformResult);
            }
            return transformResult;
        }
    }
}
