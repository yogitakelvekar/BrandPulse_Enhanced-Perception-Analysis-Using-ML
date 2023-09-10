using TermPulse.Application.Contracts.Infrastructure.Persistence;
using TermPulse.Domain.Entities;
using TermPulse.SocialMediaData.TransformWorker.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Persistence.Repositories
{
    public class PostWordCloudDataRepository : BaseSqlRepository<PostWordCloudData>, IPostWordCloudDataRepository
    {
        public PostWordCloudDataRepository(TermPulseSqlDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PostWordCloudData>> GetPostContentByPostDetail(List<PostDetail> postDetails)
        {
            var postDetailIds = postDetails.Select(pd => pd.Id).ToList();

            var data = await _dbContext.PostWordCloudData
                                       .Where(psd => postDetailIds.Contains(psd.PostDetailId))
                                       .ToListAsync();

            return data;
        }
    }
}
