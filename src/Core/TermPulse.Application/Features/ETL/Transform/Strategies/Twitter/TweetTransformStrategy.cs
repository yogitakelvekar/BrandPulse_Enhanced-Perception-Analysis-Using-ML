using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia.Tweeter;
using TermPulse.Domain.SocialMedia.Youtube;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Features.ETL.Transform.Strategies.Reddit
{
    public class TweetTransformStrategy : ITransformStrategy
    {
        public IEnumerable<Tweet> Data { get; set; }

        private readonly IPostDataTransform<Tweet> postData;
        private readonly ISentimentDataTransform<Tweet> sentimentData;
        private readonly IWordCloudDataTransform<Tweet> wordCloudData;
        private readonly IInfluencerDataTransform<Tweet> influencerData;

        public TweetTransformStrategy(IPostDataTransform<Tweet> postData, ISentimentDataTransform<Tweet> sentimentData, 
            IWordCloudDataTransform<Tweet> wordCloudData, IInfluencerDataTransform<Tweet> influencerData)
        {
            this.postData = postData;
            this.sentimentData = sentimentData;
            this.wordCloudData = wordCloudData;
            this.influencerData = influencerData;
        }

        public async Task<FinalTransformResult> TransformAsync()
        {
            var postDataResult = await postData.TransformAsync(Data);
            var sentimentDataTask = sentimentData.TransformAsync(Data, postDataResult);
            var wordCloudDataTask = wordCloudData.TransformAsync(Data, postDataResult);
            var influencerDataTask = influencerData.TransformAsync(Data, postDataResult);

            await Task.WhenAll(sentimentDataTask, wordCloudDataTask, influencerDataTask);

            var results = new FinalTransformResult();
            results.AddPostDataTransformResult(postDataResult);
            results.AddSentimentTransformResult(sentimentDataTask.Result);
            results.AddWordCloudTransformResult(wordCloudDataTask.Result);
            results.AddInfluencerTransformResult(influencerDataTask.Result);
            return results;
        }
    }
}
