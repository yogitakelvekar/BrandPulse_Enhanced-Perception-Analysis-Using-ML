using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Domain.Entities;
using BrandPulse.SocialMediaData.TransformWorker.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Persistence.Repositories
{
    public class PostDetailRepository : BaseSqlRepository<PostDetail>, IPostDetailRepository
    {
        public PostDetailRepository(BrandPulseSqlDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PostDetail>> GetPostDetailBySearchId(string searchId)
        {
            var data = await _dbContext.PostDetail.Where(pd => pd.SearchTermId == searchId).ToListAsync();
            return data;
        }
    }
}
