using BrandPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Contracts.Infrastructure.Persistence
{
    public interface IPostSentimentDataRepository : IAsyncRepository<PostSentimentData>
    {
    }
}
