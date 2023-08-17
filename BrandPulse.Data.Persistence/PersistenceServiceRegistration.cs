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

            services.AddScoped(sp => new BrandPulseMongoDbContext(mongoDBSetting.ConnectionString, mongoDBSetting.Database, mongoDBSetting.Collection));

            services.AddDbContext<BrandPulseSqlDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BrandPulseSQL")), ServiceLifetime.Transient);

            //services.AddDbContextPool<BrandPulseSqlDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("BrandPulseSQL")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseSqlRepository<>));
            services.AddScoped<ISocialMediaAggregateRepository, SocialMediaAggregateRepository>();
            services.AddScoped<IPostInfluencerDataRepository, PostInfluencerDataRepository>();
            services.AddScoped<IPostSentimentDataRepository, PostSentimentDataRepository>();
            services.AddScoped<IPostWordCloudDataRepository, PostWordCloudDataRepository>();
            services.AddScoped<IPostSentimentAnalysisRepository, PostSentimentAnalysisRepository>();
            services.AddScoped<IPostWordCloudAnalysisRepository, PostWordCloudAnalysisRepository>();
            services.AddScoped<IPostSearchDetailRepository, PostSearchDetailRepository>();
            
            return services;
        }
    }
}
