using TermPulse.Domain.SocialMedia.Youtube;

namespace TermPulse.Application.Contracts.Infrastructure.HttpServices
{
    public interface IYouTubeHttpService
    {
        Task<IEnumerable<YouTubeVideo>> SearchAndRetrieveVideoDataAsync(string searchTerm);
    }
}
