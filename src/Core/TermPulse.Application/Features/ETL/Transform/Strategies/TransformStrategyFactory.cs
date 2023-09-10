using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Features.ETL.Transform.Strategies.Reddit;
using TermPulse.Domain.Collections;
using TermPulse.Domain.SocialMedia.Reddit;
using TermPulse.Domain.SocialMedia.Tweeter;
using TermPulse.Domain.SocialMedia.Youtube;
using Microsoft.Extensions.DependencyInjection;

namespace TermPulse.Application.Features.ETL.Transform.Strategies
{
    public class TransformStrategyFactory : ITransformStrategyFactory
    {
        private readonly IServiceProvider serviceProvider;

        public TransformStrategyFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<ITransformStrategy> GetStrategies(SocialMediaAggregates data)
        {
            List<ITransformStrategy> strategies = new List<ITransformStrategy>();

            if (data?.Tweets != null)
            {
                var tweetStrategy = new TweetTransformStrategy(serviceProvider.GetRequiredService<IPostDataTransform<Tweet>>(),
                    serviceProvider.GetRequiredService<ISentimentDataTransform<Tweet>>(), 
                    serviceProvider.GetRequiredService<IWordCloudDataTransform<Tweet>>(), serviceProvider.GetRequiredService<IInfluencerDataTransform<Tweet>>());
                tweetStrategy.Data = data.Tweets;
                strategies.Add(tweetStrategy);
            }

            if (data?.RedditPosts != null)
            {
                var redditStrategy = new RedditTransformStrategy(serviceProvider.GetRequiredService<IPostDataTransform<RedditPost>>(),
                    serviceProvider.GetRequiredService<ISentimentDataTransform<RedditPost>>(), 
                    serviceProvider.GetRequiredService<IInfluencerDataTransform<RedditPost>>());
                redditStrategy.Data = data.RedditPosts;
                strategies.Add(redditStrategy);
            }

            if (data?.YouTubeVideos != null)
            {
                var youtubeStrategy = new YoutubeTransformStrategy(serviceProvider.GetRequiredService<IPostDataTransform<YouTubeVideo>>(), 
                    serviceProvider.GetRequiredService<ISentimentDataTransform<YouTubeVideo>>(), 
                    serviceProvider.GetRequiredService<IWordCloudDataTransform<YouTubeVideo>>(), 
                    serviceProvider.GetRequiredService<IInfluencerDataTransform<YouTubeVideo>>());
                youtubeStrategy.Data = data.YouTubeVideos;
                strategies.Add(youtubeStrategy);
            }

            return strategies;
        }
    }
}
