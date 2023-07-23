using BrandPulse.SocialMediaData.API.Models.Response.Services;
using BrandPulse.SocialMediaData.API.Settings;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Services;
using System.Net;

namespace BrandPulse.SocialMediaData.API.Services.HttpServices
{
    public class YouTubeHttpService
    {
        private readonly YouTubeService _youtubeService;
        private readonly ApplicationSettings _appSettings;

        public YouTubeHttpService(IOptions<ApplicationSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _appSettings.YouTubeSettings.ApiKey,
                ApplicationName = _appSettings.YouTubeSettings.ApplicationName
            });
        }

        public async Task<IEnumerable<YouTubeVideoData>> SearchAndRetrieveVideoDataAsync(string searchTerm, int maxResults = 5)
        {
            var searchItems = await SearchVideosAsync(searchTerm, maxResults);

            var videoDataTasks = searchItems.Select(async item =>
            {
                var video = await GetVideoAsync(item.Id.VideoId);
                var comments = await GetCommentsAsync(item.Id.VideoId);
                return new YouTubeVideoData
                {
                    SearchItem = item,
                    VideoDetails = video,
                    Comments = comments
                };
            });

            var videoDataList = await Task.WhenAll(videoDataTasks);

            return videoDataList;
        }

        private async Task<IList<Google.Apis.YouTube.v3.Data.SearchResult>> SearchVideosAsync(string searchTerm, int maxResults)
        {
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.Q = searchTerm;
            searchListRequest.MaxResults = maxResults;
            searchListRequest.Type = "video";
            var searchListResponse = await searchListRequest.ExecuteAsync();
            return searchListResponse.Items;
        }

        private async Task<Video?> GetVideoAsync(string videoId)
        {
            if (string.IsNullOrEmpty(videoId))
                return null;
            var videoRequest = _youtubeService.Videos.List("snippet,statistics");
            videoRequest.Id = videoId;
            var videoResponse = await videoRequest.ExecuteAsync();
            return videoResponse.Items.FirstOrDefault();
        }

        private async Task<List<CommentThread>> GetCommentsAsync(string videoId)
        {
            try
            {
                var commentsRequest = _youtubeService.CommentThreads.List("snippet,replies");
                commentsRequest.VideoId = videoId;
                commentsRequest.MaxResults = 100;
                var commentsResponse = await commentsRequest.ExecuteAsync();
                return commentsResponse.Items.ToList();
            }
            catch (Google.GoogleApiException ex)
            {
                if (ex.HttpStatusCode == HttpStatusCode.Forbidden)
                {
                    // Log the error message, or handle in a way that suits your application
                    Console.WriteLine($"Unable to fetch comments for video {videoId} because comments are disabled.");
                }
                else
                {
                    // Re-throw the exception if it's not the specific one we're trying to catch.
                    throw;
                }
            }

            // Return an empty list if no comments or comments are disabled.
            return new List<CommentThread>();
        }

    }
}
