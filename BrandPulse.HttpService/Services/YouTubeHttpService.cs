using BrandPulse.Application.Contracts.Infrastructure.HttpServices;
using BrandPulse.Domain.SocialMedia.Youtube;
using BrandPulse.HttpServices.Settings;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace BrandPulse.HttpServices.Services
{

    public class YouTubeHttpService : IYouTubeHttpService
    {
        private readonly YouTubeService _youtubeService;
        private readonly ILogger<YouTubeHttpService> _logger;
        private readonly HttpServicesSettings _appSettings;

        public YouTubeHttpService(IOptions<HttpServicesSettings> appSettings, YouTubeService youtubeService, ILogger<YouTubeHttpService> logger)
        {
            _appSettings = appSettings.Value;
            _youtubeService = youtubeService;
            _logger = logger;
        }

        public async Task<IEnumerable<YouTubeVideo>> SearchAndRetrieveVideoDataAsync(string searchTerm)
        {
            try
            {
                var searchItems = await SearchVideosAsync(searchTerm, _appSettings.MaxResults);

                var videoDataTasks = searchItems.Select(async videoId =>
                {
                    try
                    {
                        var video = await GetVideoAsync(videoId);
                        if (video == null)
                        {
                            // Log or throw appropriate exception
                            return null;
                        }

                        var channel = await GetChannelAsync(video.Snippet.ChannelId);
                        var comments = await GetCommentsAsync(videoId, _appSettings.MaxComments);

                        return new YouTubeVideo
                        {
                            VideoId = videoId,
                            Video = video,
                            Channel = channel,
                            Comments = comments
                        };
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message, ex);
                        return null;
                    }
                });
                var videoDataList = await Task.WhenAll(videoDataTasks);
                return videoDataList.Where(video => video != null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        private async Task<IList<string>> SearchVideosAsync(string searchTerm, int maxResults)
        {
            var searchListRequest = _youtubeService.Search.List("id");
            searchListRequest.Q = searchTerm;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            searchListRequest.PublishedAfter = DateTime.UtcNow.AddDays(-30);
            searchListRequest.MaxResults = maxResults;
            searchListRequest.Type = "video";
            var searchListResponse = await searchListRequest.ExecuteAsync();
            var searchVideoIds = searchListResponse.Items.Select(a => a.Id.VideoId).ToList();
            return searchVideoIds;
        }

        private async Task<Video?> GetVideoAsync(string videoId)
        {
            try
            {
                if (string.IsNullOrEmpty(videoId))
                    return null;
                var videoRequest = _youtubeService.Videos.List("snippet,statistics");
                videoRequest.Id = videoId;
                var videoResponse = await videoRequest.ExecuteAsync();
                return videoResponse.Items.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        private async Task<Channel?> GetChannelAsync(string channelId)
        {
            try
            {
                var channelRequest = _youtubeService.Channels.List("snippet,statistics");
                channelRequest.Id = channelId;
                channelRequest.Fields = "items(id,snippet(title,description,country,defaultLanguage,thumbnails),statistics)";
                var channelResponse = await channelRequest.ExecuteAsync();
                var channel = channelResponse.Items.FirstOrDefault();
                return channel;
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        private async Task<List<CommentThread>> GetCommentsAsync(string videoId, int maxResults)
        {
            try
            {
                var commentsRequest = _youtubeService.CommentThreads.List("snippet,replies");
                commentsRequest.VideoId = videoId;
                commentsRequest.MaxResults = maxResults;
                commentsRequest.Order = CommentThreadsResource.ListRequest.OrderEnum.Relevance;
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
                    Console.WriteLine($"Unable to fetch comments for video {videoId}");
                }
                return null;
            }
        }

    }
}
