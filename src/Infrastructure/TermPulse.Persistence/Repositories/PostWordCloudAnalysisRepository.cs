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
    public class PostWordCloudAnalysisRepository : BaseSqlRepository<PostWordCloudAnalysis>, IPostWordCloudAnalysisRepository
    {
        public PostWordCloudAnalysisRepository(TermPulseSqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
