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

        public async Task<FinalTransformResult> TransformAsync(SocialMediaAggregates data)
        {
            FinalTransformResult transformResult = new FinalTransformResult();
            var transformStrategies = transformStrategyFactory.GetStrategies(data);
            foreach (var strategy in transformStrategies)
            {
                var result = await strategy.TransformAsync();
                transformResult.AddSentimentTransformResult(result.SentimentTransformResult);
                transformResult.AddWordCloudTransformResult(result.WordCloudTransformResult);
                transformResult.AddInfluencerTransformResult(result.InfluencerTransformResult);
            }
            transformResult.AddSearchTerm(data.Id);
            return transformResult;
        }
    }
}
