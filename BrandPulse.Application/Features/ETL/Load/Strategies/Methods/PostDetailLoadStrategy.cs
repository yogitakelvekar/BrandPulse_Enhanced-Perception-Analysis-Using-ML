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
    public class PostDetailLoadStrategy : ILoadStrategy<PostDetailTransformResult, PostDetail>
    {

        private readonly IPostDetailRepository dataRepository;

        public PostDetailLoadStrategy(IPostDetailRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public async Task<IEnumerable<PostDetail>> LoadAsync(IEnumerable<PostDetailTransformResult> data)
        {
            var insertedData = new List<PostDetail>();
            foreach (var transformResult in data)
            {
                var entity = new PostDetail()
                {
                    Id = transformResult.Id,
                    SearchTermId = transformResult.SearchTermId,
                    PlatformId = transformResult.PlatformId,
                    PostId = transformResult.PostId,
                    PostTitle = transformResult.PostTitle,
                    PostDescription = transformResult.PostDescription,
                    PostUrl = transformResult.PostUrl,
                    PostThumbnail = transformResult.PostThumbnail,
                    PublishDate = transformResult.PublishDate,
                    PostAuthor = transformResult.PostAuthor,
                    PostAuthorAvatar = transformResult.PostAuthorAvatar,
                    Location = transformResult.Location,
                };
                insertedData.Add(entity);
            }      
            await dataRepository.BulkInsertAsync(insertedData);
            return insertedData;
        }
    }
}
