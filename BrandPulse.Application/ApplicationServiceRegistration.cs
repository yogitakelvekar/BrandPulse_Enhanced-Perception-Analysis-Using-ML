using BrandPulse.Application.Contracts.Features.DataSearch;
using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using BrandPulse.Application.Contracts.Features.ETL.Transform;
using BrandPulse.Application.Features.DataSearch;
using BrandPulse.Application.Features.ETL.Transform.Strategies;
using BrandPulse.Application.Features.ETL.Transform;
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
using BrandPulse.Application.Contracts.Features.ETL;
using BrandPulse.Application.Features.ETL;
using BrandPulse.Application.Contracts.Features.ETL.Extract;
using BrandPulse.Application.Features.ETL.Extract;
using BrandPulse.Domain.SocialMedia.Tweeter;
using BrandPulse.Domain.SocialMedia.Reddit;
using BrandPulse.Domain.SocialMedia.Youtube;

namespace BrandPulse.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataSearchFeatureServices(configuration);
            services.AddETLFeatureServices(configuration);
            return services;
        }

        private static IServiceCollection AddDataSearchFeatureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISocialMediaSearch, SocialMediaSearch>();
            return services;
        }

        private static IServiceCollection AddETLFeatureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExtractServices(configuration);
            services.AddTransformServices(configuration);
            services.AddTransient<IETLWorkflowManager, ETLWorkflowManager>();
            return services;
        }

        private static IServiceCollection AddExtractServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISearchDataExtract, SocialMediaDataExtract>();
            return services;
        }

        private static IServiceCollection AddTransformServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Transform services.
            services.AddTransient<ITransformStrategyFactory, TransformStrategyFactory>();
            services.AddTransient<ISearchDataTransform, SearchDataTransform>();        

            // Register SentimentDataTransform and WordCloudDataTransform for each relevant type.
            services.AddTransient<ISentimentDataTransform<Tweet>, TweetSentimentDataTransform>();
            services.AddTransient<IWordCloudDataTransform<Tweet>, TweetWordCloudDataTransform>();
            services.AddTransient<IInfluencerDataTransform<Tweet>, TweetInfluencerDataTransform>();

            services.AddTransient<ISentimentDataTransform<RedditPost>, RedditSentimentDataTransform>();
            services.AddTransient<IInfluencerDataTransform<RedditPost>, RedditInfluencerDataTransform>();

            services.AddTransient<ISentimentDataTransform<YouTubeVideo>, YoutubeSentimentDataTransform>();
            services.AddTransient<IWordCloudDataTransform<YouTubeVideo>, YoutubeWordCloudDataTransform>();
            services.AddTransient<IInfluencerDataTransform<YouTubeVideo>, YoutubeInfluencerDataTransform>();

            return services;
        }
    }
}
