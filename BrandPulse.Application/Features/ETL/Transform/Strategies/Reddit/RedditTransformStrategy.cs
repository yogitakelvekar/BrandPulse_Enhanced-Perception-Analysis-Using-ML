using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia.Reddit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Reddit
{
    public class RedditTransformStrategy : ITransformStrategy
    {
        public IEnumerable<RedditPost>? Data { get; set; }
        private readonly ISentimentDataTransform<RedditPost> sentimentData;
        private readonly IInfluencerDataTransform<RedditPost> influencerData;

        public RedditTransformStrategy(ISentimentDataTransform<RedditPost> sentimentData, IInfluencerDataTransform<RedditPost> influencerData)
        {
            this.sentimentData = sentimentData;
            this.influencerData = influencerData;
        }

        public async Task<FinalTransformResult> TransformAsync()
        {
            var sentimentDataResult = await sentimentData.TransformAsync(Data);
            var influencerDataResult = await influencerData.TransformAsync(Data);

            var results = new FinalTransformResult();
            results.AddSentimentTransformResult(sentimentDataResult);
            results.AddInfluencerTransformResult(influencerDataResult);
            return results;
        }
    }
}
