using BrandPulse.Application.Contracts.Features.DataSearch;
using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using BrandPulse.Application.Contracts.Features.ETL.Transform;
using BrandPulse.Application.Features.DataSearch;
using BrandPulse.Application.Features.ETL.Transform.Strategies;
using BrandPulse.Application.Features.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandPulse.Application.Features.ETL.Transform.Strategies.Twitter.Methods;
using BrandPulse.Application.Features.ETL.Transform.Strategies.Reddit.Methods;
using BrandPulse.Application.Features.ETL.Transform.Strategies.Youtube.Methods;

namespace BrandPulse.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExtractServices(configuration);
            services.AddTransformServices(configuration);
            services.AddScoped<ISearchDataTransform, SearchDataTransform>();
            return services;
        }

        private static IServiceCollection AddExtractServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISocialMediaSearch, SocialMediaSearch>();
            return services;
        }

        private static IServiceCollection AddTransformServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Transform services.
            services.AddScoped<ITransformStrategyFactory, TransformStrategyFactory>();
            services.AddScoped<ISearchDataTransform, SearchDataTransform>();

            // Register SentimentDataTransform and WordCloudDataTransform for each relevant type.
            services.AddScoped<ISentimentDataTransform<Tweet>, TweetSentimentDataTransform>();
            services.AddScoped<IWordCloudDataTransform<Tweet>, TweetWordCloudDataTransform>();

            services.AddScoped<ISentimentDataTransform<RedditPost>, RedditSentimentDataTransform>();
         
            services.AddScoped<ISentimentDataTransform<YouTubeVideo>, YoutubeSentimentDataTransform>();
            services.AddScoped<IWordCloudDataTransform<YouTubeVideo>, YoutubeWordCloudDataTransform>();

            return services;
        }
    }
}
