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
    public class RedditTransformStrategy : ITransformStrategy
    {
        public IEnumerable<RedditPost> Data { get; set; }
        private readonly ISentimentDataTransform<RedditPost> sentimentData;

        public RedditTransformStrategy(ISentimentDataTransform<RedditPost> sentimentData)
        {
            this.sentimentData = sentimentData;         
        }

        public async Task<FinalTransformResult> TransformAsync()
        {
            var data = await sentimentData.TransformAsync(Data);

            var results = new FinalTransformResult();
            results.AddSentimentTransformResult(data);
            return results;
        }
    }
}
