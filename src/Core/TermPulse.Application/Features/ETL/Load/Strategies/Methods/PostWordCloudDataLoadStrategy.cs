using TermPulse.Application.Contracts.Features.ETL.Load;
using TermPulse.Application.Contracts.Infrastructure.Persistence;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Features.ETL.Load.Strategies.Methods
{
    public class PostWordCloudDataLoadStrategy : ILoadStrategy<WordCloudTransformResult, PostWordCloudData>
    {
        private readonly IPostWordCloudDataRepository dataRepository;

        public PostWordCloudDataLoadStrategy(IPostWordCloudDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<IEnumerable<PostWordCloudData>> LoadAsync(IEnumerable<WordCloudTransformResult> data)
        {
            var insertedData = new List<PostWordCloudData>();
            foreach (var transformResult in data)
            {
                var entity = new PostWordCloudData()
                {
                    Id = Guid.NewGuid(),
                    PostDetailId = transformResult.PostDetailId,
                    Hashtags = string.Join(",", transformResult.Hashtags),                  
                };
                insertedData.Add(entity);
            }
            // Using the BulkInsertAsync method for improved bulk insertion performance.
            await dataRepository.BulkInsertAsync(insertedData);
            return insertedData;
        }
    }
}
