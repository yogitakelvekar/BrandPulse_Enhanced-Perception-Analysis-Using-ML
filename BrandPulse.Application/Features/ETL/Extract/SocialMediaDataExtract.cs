using BrandPulse.Application.Contracts.Features.ETL.Extract;
using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Domain.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Extract
{
    public class SocialMediaDataExtract : ISearchDataExtract
    {
        private readonly ISocialMediaAggregateRepository mediaAggregateRepository;

        public SocialMediaDataExtract(ISocialMediaAggregateRepository mediaAggregateRepository)
        {
            this.mediaAggregateRepository = mediaAggregateRepository;
        }

        public async Task<SocialMediaAggregates> ExtractAsync(string searchId)
        {
            var result = await mediaAggregateRepository.GetDataAsync(searchId);
            return result;
        }
    }
}
