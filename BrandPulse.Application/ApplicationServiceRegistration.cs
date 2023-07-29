using BrandPulse.Application.Contracts.Features.ETL.Extract;
using BrandPulse.Application.Features.ETL.Extract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISocialMediaAggregate, SocialMediaAggregate>();
            return services;
        }
    }
}
