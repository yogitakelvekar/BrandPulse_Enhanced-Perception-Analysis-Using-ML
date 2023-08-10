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
using BrandPulse.Application.Contracts.Features.ETL.Load;
using BrandPulse.Application.Features.ETL.Load;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.Entities;
using BrandPulse.Application.Features.ETL.Load.Strategies.Methods;

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
            services.AddScoped<ISocialMediaSearch, SocialMediaSearch>();
            return services;
        }

        private static IServiceCollection AddETLFeatureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExtractServices(configuration);
            services.AddTransformServices(configuration);
            services.AddLoadServices(configuration);
            services.AddScoped<IETLWorkflowManager, ETLWorkflowManager>();
            return services;
        }

        private static IServiceCollection AddExtractServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISearchDataExtract, SocialMediaDataExtract>();
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
            services.AddScoped<IInfluencerDataTransform<Tweet>, TweetInfluencerDataTransform>();

            services.AddScoped<ISentimentDataTransform<RedditPost>, RedditSentimentDataTransform>();
            services.AddScoped<IInfluencerDataTransform<RedditPost>, RedditInfluencerDataTransform>();

            services.AddScoped<ISentimentDataTransform<YouTubeVideo>, YoutubeSentimentDataTransform>();
            services.AddScoped<IWordCloudDataTransform<YouTubeVideo>, YoutubeWordCloudDataTransform>();
            services.AddScoped<IInfluencerDataTransform<YouTubeVideo>, YoutubeInfluencerDataTransform>();

            return services;
        }

        private static IServiceCollection AddLoadServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISearchDataLoad, SearchDataLoad>();
            services.AddScoped<ILoadStrategy<SentimentTransformResult, PostSentimentData>, PostSentimentDataLoadStrategy>();
            services.AddScoped<ILoadStrategy<WordCloudTransformResult, PostWordCloudData>, PostWordCloudDataLoadStrategy>();
            services.AddScoped<ILoadStrategy<InfluencerTransformResult, PostInfluencerData>, PostInfluencerDataLoadStrategy>();
            return services;
        }
    }
}
