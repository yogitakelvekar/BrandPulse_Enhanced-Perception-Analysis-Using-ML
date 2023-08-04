using BrandPulse.Domain.SocialMedia.Reddit;

namespace BrandPulse.Application.Contracts.Infrastructure.HttpServices
{
    public interface IRedditHttpService
    {
        Task<IEnumerable<RedditPost>?> SearchPosts(string searchTerm);
    }
}
