using TermPulse.Application.Contracts.Infrastructure.Persistence;
using TermPulse.Persistence.Repositories;
using TermPulse.Persistence.Settings;
using TermPulse.SocialMediaData.TransformWorker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TermPulse.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDBSetting = configuration.GetRequiredSection("MongoDBSettings").Get<MongoDBSettings>();

            services.AddScoped(sp => new TermPulseMongoDbContext(mongoDBSetting.ConnectionString, mongoDBSetting.Database, mongoDBSetting.Collection));

            services.AddDbContext<TermPulseSqlDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TermPulseSQL")), ServiceLifetime.Transient);

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseSqlRepository<>));
            services.AddScoped<ISocialMediaAggregateRepository, SocialMediaAggregateRepository>();
            services.AddScoped<IPostInfluencerDataRepository, PostInfluencerDataRepository>();
            services.AddScoped<IPostSentimentDataRepository, PostSentimentDataRepository>();
            services.AddScoped<IPostWordCloudDataRepository, PostWordCloudDataRepository>();
            services.AddScoped<IPostSentimentAnalysisRepository, PostSentimentAnalysisRepository>();
            services.AddScoped<IPostWordCloudAnalysisRepository, PostWordCloudAnalysisRepository>();
            services.AddScoped<IPostSearchDetailRepository, PostSearchDetailRepository>();
            services.AddScoped<IPostDetailRepository, PostDetailRepository>();

            return services;
        }
    }
}
