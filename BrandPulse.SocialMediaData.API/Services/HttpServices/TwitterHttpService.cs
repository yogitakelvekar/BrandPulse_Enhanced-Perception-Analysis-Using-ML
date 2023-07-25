using BrandPulse.SocialMediaData.API.Models.Response.Services;
using BrandPulse.SocialMediaData.API.Models.Response.Services.BrandPulse.SocialMediaData.API.Models.Response.Services;
using BrandPulse.SocialMediaData.API.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BrandPulse.SocialMediaData.API.Services.HttpServices
{
    public class TwitterHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationSettings _appSettings;

        public TwitterHttpService(HttpClient httpClient, IOptions<ApplicationSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<IEnumerable<Tweet>> SearchTweetsAsync(string query)
        {
            var response = await _httpClient.GetAsync($"v1.1/SearchTweets/?q={query}&count={_appSettings.MaxResults}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TweetResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var finalResponse = result?.statuses.ToList() ?? new List<Tweet>();
            return finalResponse;
        }

        public async Task<JsonDocument> SearchTweetsAsyncObject(string query)
        {
            var response = await _httpClient.GetAsync($"v1.1/SearchTweets/?q={query}&count={_appSettings.MaxResults}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return JsonDocument.Parse(content);
        }
    }
}
