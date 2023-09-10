using TermPulse.Application.Contracts.Features.DataSearch;
using TermPulse.Application.Contracts.Infrastructure.HttpServices;
using TermPulse.Application.Contracts.Infrastructure.Persistence;
using TermPulse.Domain.Collections;

namespace TermPulse.Application.Features.DataSearch
{
    public class SocialMediaSearch : ISocialMediaSearch
    {
        private readonly IYouTubeHttpService youTubeHttpService;
        private readonly IRedditHttpService redditHttpService;
        private readonly ITwitterHttpService twitterHttpService;
        private readonly ISocialMediaAggregateRepository aggregateRepository;

        public SocialMediaSearch(IYouTubeHttpService youTubeHttpService, IRedditHttpService redditHttpService, ITwitterHttpService twitterHttpService,
            ISocialMediaAggregateRepository aggregateRepository)
        {
            this.youTubeHttpService = youTubeHttpService;
            this.redditHttpService = redditHttpService;
            this.twitterHttpService = twitterHttpService;
            this.aggregateRepository = aggregateRepository;
        }

        public async Task<SocialMediaAggregates> SearchAllAndStore(string searchTerm)
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
