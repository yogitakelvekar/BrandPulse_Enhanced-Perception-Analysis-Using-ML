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
                    PostAuthorProfile = transformResult.PostAuthorProfile,
                    Location = transformResult.Location,
                };
                insertedData.Add(entity);
            }      
            await dataRepository.BulkInsertAsync(insertedData);
            return insertedData;
        }
    }
}
