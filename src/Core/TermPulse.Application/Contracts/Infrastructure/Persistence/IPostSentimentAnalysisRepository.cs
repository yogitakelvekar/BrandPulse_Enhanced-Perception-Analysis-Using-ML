using TermPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Contracts.Infrastructure.Persistence
{
    public interface IPostSentimentAnalysisRepository : IAsyncRepository<PostSentimentAnalysis>
    {

    }
}
