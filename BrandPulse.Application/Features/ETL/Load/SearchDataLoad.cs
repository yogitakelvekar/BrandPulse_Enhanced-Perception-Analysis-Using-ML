using BrandPulse.Application.Contracts.Features.ETL.Load;
using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Load
{
    public class SearchDataLoad : ISearchDataLoad
    {
        private readonly ILoadStrategy<WordCloudTransformResult, PostWordCloudData> wordCloud;
        private readonly ILoadStrategy<SentimentTransformResult, PostSentimentData> sentiment;
        private readonly ILoadStrategy<InfluencerTransformResult, PostInfluencerData> influencer;

        public SearchDataLoad(ILoadStrategy<WordCloudTransformResult, PostWordCloudData> wordCloud,
            ILoadStrategy<SentimentTransformResult, PostSentimentData> sentiment,
            ILoadStrategy<InfluencerTransformResult, PostInfluencerData> influencer)
        {
            this.wordCloud = wordCloud;
            this.sentiment = sentiment;
            this.influencer = influencer;
        }

        public async Task<bool> LoadAsync(FinalTransformResult transformResult)
        {
            bool result;
            try
            {
                var wordCloudTask = wordCloud.LoadAsync(transformResult.WordCloudTransformResult);
                var sentimentTask = sentiment.LoadAsync(transformResult.SentimentTransformResult);
                var influencerTask = influencer.LoadAsync(transformResult.InfluencerTransformResult);

                Task.WaitAll(wordCloudTask, sentimentTask, influencerTask);

                result = true;
            }
            catch (Exception ex)
            {
                // You might want to log the exception here for debugging purposes.
                result = false;
            }
            return result;
        }
    }
}
