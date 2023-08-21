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
    public class PostInfluencerDataLoadStrategy : ILoadStrategy<InfluencerTransformResult, PostInfluencerData>
    {
        private readonly IPostInfluencerDataRepository dataRepository;

        public PostInfluencerDataLoadStrategy(IPostInfluencerDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public async Task<IEnumerable<PostInfluencerData>> LoadAsync(IEnumerable<InfluencerTransformResult> data)
        {
            var insertedData = new List<PostInfluencerData>();
            foreach (var transformResult in data)
            {
                var entity = new PostInfluencerData()
                {
                    Id = Guid.NewGuid(),
                    PostDetailId = transformResult.PostDetailId,
                    Engagement = transformResult.Engagement,
                    PotentialReach = transformResult.PotentialReach,
                };
                insertedData.Add(entity);
            }
            // Using the BulkInsertAsync method for improved bulk insertion performance.
            await dataRepository.BulkInsertAsync(insertedData);
            return insertedData;
        }
    }
}
