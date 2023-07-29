using BrandPulse.Domain.SocialMedia;

namespace BrandPulse.Application.Contracts.Infrastructure.HttpServices
{
    public interface IYouTubeHttpService
    {
        Task<IEnumerable<YouTubeVideo>> SearchAndRetrieveVideoDataAsync(string searchTerm);
    }
}
