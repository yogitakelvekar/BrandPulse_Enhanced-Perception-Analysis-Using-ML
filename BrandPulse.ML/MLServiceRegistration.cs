using BrandPulse.Application.Contracts.Features.DataScience.DataProcessors;
using BrandPulse.ML.Source.DataProcessors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.ML
{
    public static class MLServiceRegistration
    {
        public static IServiceCollection AddMLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISentimentDataProcessor, SentimentDataProcessor>();
            return services;
        }
            
    }
}
