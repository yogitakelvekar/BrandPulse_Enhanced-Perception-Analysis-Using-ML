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
    public class PostSentimentDataRepository : BaseSqlRepository<PostSentimentData>, IPostSentimentDataRepository
    {
        public PostSentimentDataRepository(BrandPulseSqlDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PostSentimentData>> GetPostContentBySearchId(string searchId)
        {
            var data = await _dbContext.PostSentimentData.Where(psd => psd.SearchTermId == searchId && psd.PostContent != null).ToListAsync();
            return data;
        }
    }
}
