using TermPulse.Application.Contracts.Infrastructure.HttpServices;
using TermPulse.Domain.SocialMedia.Tweeter;
using TermPulse.HttpServices.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace TermPulse.HttpServices.Services
{
    public class TwitterHttpService : ITwitterHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TwitterHttpService> _logger;
        private readonly HttpServicesSettings _appSettings;

        public TwitterHttpService(HttpClient httpClient, IOptions<HttpServicesSettings> appSettings, ILogger<TwitterHttpService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<IEnumerable<Tweet>> SearchTweetsAsync(string query)
        {
            try
            {
                var response = await _httpClient.GetAsync($"v1.1/SearchTweets/?q={query}&result_type=recent&count={_appSettings.MaxResults}");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    IgnoreNullValues = true,
                    IgnoreReadOnlyProperties = true
                };

                var result = JsonSerializer.Deserialize<TweetResponse>(content, options);
                var finalResponse = result?.statuses.ToList() ?? new List<Tweet>();
                return finalResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
           
        }

        public async Task<TwitterUser> GetUserDetails(string userId)
        {
            var response = await _httpClient.GetAsync($"v1.1/Users/?ids={userId}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            };

            var result = JsonSerializer.Deserialize<IEnumerable<TwitterUser>>(content, options);
            var finalResponse = result?.FirstOrDefault() ?? new TwitterUser();
            return finalResponse;
        }


    }
}
