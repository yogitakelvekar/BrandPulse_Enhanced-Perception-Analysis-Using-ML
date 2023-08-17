using BrandPulse.Application.Contracts.Infrastructure.HttpServices;
using BrandPulse.Domain.SocialMedia.Tweeter;
using BrandPulse.HttpServices.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BrandPulse.HttpServices.Services
{
    public class TwitterHttpService : ITwitterHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpServicesSettings _appSettings;

        public TwitterHttpService(HttpClient httpClient, IOptions<HttpServicesSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<IEnumerable<Tweet>> SearchTweetsAsync(string query)
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
