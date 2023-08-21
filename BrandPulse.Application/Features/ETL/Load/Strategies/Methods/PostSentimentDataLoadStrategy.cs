using BrandPulse.Application.Contracts.Features.ETL.Load;
using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Load.Strategies.Methods
{
    public class PostSentimentDataLoadStrategy : ILoadStrategy<SentimentTransformResult, PostSentimentData>
    {
        private readonly IPostSentimentDataRepository dataRepository;

        public PostSentimentDataLoadStrategy(IPostSentimentDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<IEnumerable<PostSentimentData>> LoadAsync(IEnumerable<SentimentTransformResult> data)
        {
            var insertedData = new List<PostSentimentData>();
            foreach (var transformResult in data)
            {
                var entity = new PostSentimentData()
                {
                    Id = Guid.NewGuid(),
                    PostDetailId = transformResult.PostDetailId,              
                    PostContent = transformResult.PostContent,
                    SubPostDate = transformResult.SubPostDate,
                    SubPostId = transformResult.SubPostId,                
                    PostLikes = transformResult.PostLikes,
                    PostDislikes = transformResult.PostDislikes,
                };
                insertedData.Add(entity);
            }
            // Using the BulkInsertAsync method for improved bulk insertion performance.
            await dataRepository.BulkInsertAsync(insertedData);
            return insertedData;
        }
    }
}
