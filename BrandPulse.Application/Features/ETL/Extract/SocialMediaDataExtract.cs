using Amazon.Runtime.Internal.Util;
using BrandPulse.Application.Contracts.Features.ETL.Extract;
using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Domain.Collections;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<SocialMediaDataExtract> logger;

        public SocialMediaDataExtract(ISocialMediaAggregateRepository mediaAggregateRepository, ILogger<SocialMediaDataExtract> logger)
        {
            this.mediaAggregateRepository = mediaAggregateRepository;
            this.logger = logger;
        }

        public async Task<SocialMediaAggregates> ExtractAsync(string searchId)
        {
            logger.LogInformation("Data extract method executing.");
            var result = await mediaAggregateRepository.GetDataAsync(searchId);
            return result;
        }
    }
}
