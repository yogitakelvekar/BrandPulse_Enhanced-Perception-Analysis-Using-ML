using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
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
        public Task<IEnumerable<InfluencerTransformResult>> TransformAsync(IEnumerable<RedditPost> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var postResults = data
                .Where(post => post.GetType() == typeof(RedditPost))
                .Select(post => new InfluencerTransformResult
                {                   
                    PotentialReach = post?.User?.PostKarma ?? 0,
                    Engagement = post.UpVotes + post.DownVotes + post.Comments.Count(),
                    PostDetailId = postDetails.First(pd => pd.PostId == post.Id).Id,                 
                });
            return Task.FromResult(postResults.AsEnumerable());
        }
    }
}
