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
    public class PostDetailRepository : BaseSqlRepository<PostDetail>, IPostDetailRepository
    {
        public PostDetailRepository(TermPulseSqlDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PostDetail>> GetPostDetailBySearchId(string searchId)
        {
            var data = await _dbContext.PostDetail.Where(pd => pd.SearchTermId == searchId).ToListAsync();
            return data;
        }
    }
}
