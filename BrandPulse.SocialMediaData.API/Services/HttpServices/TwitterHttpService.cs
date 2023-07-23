using BrandPulse.SocialMediaData.API.Models.Response.Services;
using System.Net.Http;
using System.Text.Json;

namespace BrandPulse.SocialMediaData.API.Services.HttpServices
{
    public class TwitterHttpService
    {
        private readonly HttpClient _httpClient;

        public TwitterHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TweetResponse> SearchTweetsAsync(string query, int count = 20)
        {
            var response = await _httpClient.GetAsync($"v1.1/SearchTweets/?q={query}&count={count}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TweetResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}
