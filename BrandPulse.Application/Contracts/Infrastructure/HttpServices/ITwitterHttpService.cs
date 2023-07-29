using BrandPulse.Domain.SocialMedia;
using System.Text.Json;

namespace BrandPulse.Application.Contracts.Infrastructure.HttpServices
{
    public interface ITwitterHttpService
    {
        Task<IEnumerable<Tweet>> SearchTweetsAsync(string query);
        Task<JsonDocument> SearchTweetsAsyncObject(string query);
    }
}
