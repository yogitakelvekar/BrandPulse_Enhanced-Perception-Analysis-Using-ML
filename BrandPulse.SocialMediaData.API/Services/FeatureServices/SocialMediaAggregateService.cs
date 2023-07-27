using BrandPulse.SocialMediaData.API.Data.Repositories;
using BrandPulse.SocialMediaData.API.Models.Entities;
using BrandPulse.SocialMediaData.API.Models.Response.Services;
using BrandPulse.SocialMediaData.API.Services.HttpServices;

namespace BrandPulse.SocialMediaData.API.Services.FeatureServices
{
    public class SocialMediaAggregateService
    {
        private readonly YouTubeHttpService youTubeHttpService;
        private readonly RedditHttpService redditHttpService;
        private readonly TwitterHttpService twitterHttpService;
        private readonly SocialMediaAggregateRepository aggregateRepository;

        public SocialMediaAggregateService(YouTubeHttpService youTubeHttpService, RedditHttpService redditHttpService, TwitterHttpService twitterHttpService, SocialMediaAggregateRepository aggregateRepository) 
        {
            this.youTubeHttpService = youTubeHttpService;
            this.redditHttpService = redditHttpService;
            this.twitterHttpService = twitterHttpService;
            this.aggregateRepository = aggregateRepository;
        }

        public async Task<SocialMediaAggregates> SearchAndStore(string searchTerm)
        {
            var twitterTask = twitterHttpService.SearchTweetsAsync(searchTerm);
            var youtubeTask = youTubeHttpService.SearchAndRetrieveVideoDataAsync(searchTerm);
            var redditTask = redditHttpService.SearchPosts(searchTerm);

            await Task.WhenAll(twitterTask, youtubeTask, redditTask);

            var aggregateData = new SocialMediaAggregates
            {
                SearchTerm = searchTerm,
                Tweets = await twitterTask,
                YouTubeVideos = await youtubeTask,
                RedditPosts = await redditTask
            };

            await aggregateRepository.StoreDataAsync(aggregateData);

            return aggregateData;
        }
    }
}
