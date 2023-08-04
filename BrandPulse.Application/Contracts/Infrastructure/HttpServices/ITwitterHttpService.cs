using BrandPulse.Domain.SocialMedia.Tweeter;
using System.Text.Json;

namespace BrandPulse.Application.Contracts.Infrastructure.HttpServices
{
    public interface ITwitterHttpService
    {
        Task<IEnumerable<Tweet>> SearchTweetsAsync(string query);
        Task<TwitterUser> GetUserDetails(string userId);
    }
}
