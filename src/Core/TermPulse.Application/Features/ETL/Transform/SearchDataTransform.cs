using TermPulse.Application.Contracts.Features.ETL.Transform;
using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Features.ETL.Transform
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
            FinalTransformResult transformResult = MapPostSearchTermDetails(data);     
            var transformStrategies = transformStrategyFactory.GetStrategies(data);
            foreach (var strategy in transformStrategies)
            {
                var result = await strategy.TransformAsync();
                transformResult.AddPostDataTransformResult(result.PostDataTransformResult);
                transformResult.AddSentimentTransformResult(result.SentimentTransformResult);
                transformResult.AddWordCloudTransformResult(result.WordCloudTransformResult);
                transformResult.AddInfluencerTransformResult(result.InfluencerTransformResult);
            }         
            return transformResult;
        }

        private static FinalTransformResult MapPostSearchTermDetails(SocialMediaAggregates data)
        {
            FinalTransformResult transformResult = new FinalTransformResult();
            transformResult.SearchTermId = data.Id;
            transformResult.SearchTerm = data.SearchTerm;
            transformResult.SearchDateTime = data.SearchDateTime;
            return transformResult;
        }
    }
}
