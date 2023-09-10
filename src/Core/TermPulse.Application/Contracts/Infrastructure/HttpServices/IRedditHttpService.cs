using TermPulse.Domain.SocialMedia.Reddit;

namespace TermPulse.Application.Contracts.Infrastructure.HttpServices
{
    public interface IRedditHttpService
    {
        Task<IEnumerable<RedditPost>?> SearchPosts(string searchTerm);
    }
}
