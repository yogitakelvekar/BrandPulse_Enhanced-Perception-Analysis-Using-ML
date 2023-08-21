using BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia.Youtube;

namespace BrandPulse.Application.Features.ETL.Transform.Strategies.Youtube.Methods
{
    public class YoutubeInfluencerDataTransform : IInfluencerDataTransform<YouTubeVideo>
    {
        public Task<IEnumerable<InfluencerTransformResult>> TransformAsync(IEnumerable<YouTubeVideo> data, IEnumerable<PostDetailTransformResult> postDetails)
        {
            var result = data.Select(video => new InfluencerTransformResult
            {
                PostDetailId = postDetails.First(pd => pd.PostId == video?.VideoId).Id,
                PotentialReach = Convert.ToInt32(video?.Channel?.Statistics?.SubscriberCount ?? 0),
                Engagement = Convert.ToInt32(video?.Video?.Statistics?.ViewCount ?? 0),
            });
            return Task.FromResult(result.AsEnumerable());
        }
    }
}
