using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Persistence.Repositories;
using BrandPulse.Persistence.Settings;
using BrandPulse.SocialMediaData.TransformWorker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrandPulse.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDBSetting = configuration.GetRequiredSection("MongoDBSettings").Get<MongoDBSettings>();
            services.AddSingleton(sp => new BrandPulseMongoDbContext(mongoDBSetting.ConnectionString, mongoDBSetting.Database, mongoDBSetting.Collection));
            services.AddDbContext<BrandPulseSqlDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("BrandPulseSQL")));
            services.AddScoped<ISocialMediaAggregateRepository, SocialMediaAggregateRepository>();
            return services;
        }
    }
}
