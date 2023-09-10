using TermPulse.Application.Contracts.Infrastructure.Persistence;
using TermPulse.Domain.Entities;
using TermPulse.SocialMediaData.TransformWorker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Persistence.Repositories
{
    public class PostSentimentAnalysisRepository : BaseSqlRepository<PostSentimentAnalysis>, IPostSentimentAnalysisRepository
    {
        public PostSentimentAnalysisRepository(TermPulseSqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
