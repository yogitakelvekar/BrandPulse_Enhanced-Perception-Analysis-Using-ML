using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia.Tweeter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Reddit
{
    public class TweetTransformStrategy : ITransformStrategy
    {
        public IEnumerable<Tweet> Data { get; set; }
        private readonly ISentimentDataTransform<Tweet> sentimentData;
        private readonly IWordCloudDataTransform<Tweet> wordCloudData;
        private readonly IInfluencerDataTransform<Tweet> influencerData;

        public TweetTransformStrategy(ISentimentDataTransform<Tweet> sentimentData, 
            IWordCloudDataTransform<Tweet> wordCloudData, IInfluencerDataTransform<Tweet> influencerData)
        {
            this.sentimentData = sentimentData;
            this.wordCloudData = wordCloudData;
            this.influencerData = influencerData;
        }

        public async Task<FinalTransformResult> TransformAsync()
        {
            var sentimentDataTask = sentimentData.TransformAsync(Data);
            var wordCloudDataTask = wordCloudData.TransformAsync(Data);
            var influencerDataTask = influencerData.TransformAsync(Data);

            await Task.WhenAll(sentimentDataTask, wordCloudDataTask, influencerDataTask);

            var results = new FinalTransformResult();
            results.AddSentimentTransformResult(sentimentDataTask.Result);
            results.AddWordCloudTransformResult(wordCloudDataTask.Result);
            results.AddInfluencerTransformResult(influencerDataTask.Result);
            return results;
        }
    }
}
