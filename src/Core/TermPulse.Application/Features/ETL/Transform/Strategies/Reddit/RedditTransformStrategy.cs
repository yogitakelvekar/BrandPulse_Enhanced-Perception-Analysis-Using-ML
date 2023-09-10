using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia.Reddit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Features.ETL.Transform.Strategies.Reddit
{
    public class RedditTransformStrategy : ITransformStrategy
    {
        public IEnumerable<RedditPost>? Data { get; set; }

        private readonly IPostDataTransform<RedditPost> postData;
        private readonly ISentimentDataTransform<RedditPost> sentimentData;
        private readonly IInfluencerDataTransform<RedditPost> influencerData;

        public RedditTransformStrategy(IPostDataTransform<RedditPost> postData,ISentimentDataTransform<RedditPost> sentimentData, 
            IInfluencerDataTransform<RedditPost> influencerData)
        {
            this.postData = postData;
            this.sentimentData = sentimentData;
            this.influencerData = influencerData;
        }

        public async Task<FinalTransformResult> TransformAsync()
        {
            var postDataResult = await postData.TransformAsync(Data);
            var sentimentDataTask = sentimentData.TransformAsync(Data, postDataResult);
            var influencerDataTask = influencerData.TransformAsync(Data, postDataResult);

            await Task.WhenAll(sentimentDataTask, influencerDataTask);

            var results = new FinalTransformResult();
            results.AddPostDataTransformResult(postDataResult);
            results.AddSentimentTransformResult(sentimentDataTask.Result);
            results.AddInfluencerTransformResult(influencerDataTask.Result);
            return results;
        }
    }
}
