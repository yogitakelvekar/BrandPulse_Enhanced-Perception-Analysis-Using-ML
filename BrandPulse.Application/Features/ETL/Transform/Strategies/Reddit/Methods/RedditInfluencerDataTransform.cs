using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia.Reddit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Reddit.Methods
{
    public class RedditInfluencerDataTransform : IInfluencerDataTransform<RedditPost>
    {
        public Task<IEnumerable<InfluencerTransformResult>> TransformAsync(IEnumerable<RedditPost> data)
        {
            var postResults = data
                .Where(post => post.GetType() == typeof(RedditPost))
                .Select(post => new InfluencerTransformResult
                {
                    AuthorName = post.Author,
                    //Avatar = post.
                    //PotentialReach = post.
                    Engagement = post.UpVotes + post.DownVotes + post.Comments.Count(),
                    //Profile
                    //Country = post.Author.
                    PostId = post.Id,
                    PlatformId = 1, // Change to your specific platform Id
                    PostDate = post.Created
                });
            return Task.FromResult(postResults.AsEnumerable());
        }
    }
}
