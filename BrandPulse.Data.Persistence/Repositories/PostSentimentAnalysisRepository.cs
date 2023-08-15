using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Domain.Entities;
using BrandPulse.SocialMediaData.TransformWorker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Persistence.Repositories
{
    public class PostSentimentAnalysisRepository : BaseSqlRepository<PostSentimentAnalysis>, IPostSentimentAnalysisRepository
    {
        public PostSentimentAnalysisRepository(BrandPulseSqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
