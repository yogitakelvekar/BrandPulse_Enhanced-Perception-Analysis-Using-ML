using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
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

        public TweetTransformStrategy(ISentimentDataTransform<Tweet> sentimentData, IWordCloudDataTransform<Tweet> wordCloudData)
        {
            this.sentimentData = sentimentData;
            this.wordCloudData = wordCloudData;
        }

        public async Task<FinalTransformResult> TransformAsync()
        {
            var sentimentDataTask = sentimentData.TransformAsync(Data);
            var wordCloudDataTask = wordCloudData.TransformAsync(Data);

            await Task.WhenAll(sentimentDataTask, wordCloudDataTask);

            var results = new FinalTransformResult();
            results.AddSentimentTransformResult(sentimentDataTask.Result);
            results.AddWordCloudTransformResult(wordCloudDataTask.Result);
            return results;
        }
    }
}
