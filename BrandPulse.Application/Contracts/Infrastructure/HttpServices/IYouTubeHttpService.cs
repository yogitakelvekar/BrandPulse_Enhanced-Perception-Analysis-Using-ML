using BrandPulse.Domain.SocialMedia.Youtube;

namespace BrandPulse.Application.Contracts.Infrastructure.HttpServices
{
    public interface IYouTubeHttpService
    {
        Task<IEnumerable<YouTubeVideo>> SearchAndRetrieveVideoDataAsync(string searchTerm);
    }
}
