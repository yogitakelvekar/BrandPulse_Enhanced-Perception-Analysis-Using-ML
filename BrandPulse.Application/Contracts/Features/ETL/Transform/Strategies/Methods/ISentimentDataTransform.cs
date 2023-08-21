using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods
{
    public interface ISentimentDataTransform<TData> where TData : class
    {
        Task<IEnumerable<SentimentTransformResult>> TransformAsync(IEnumerable<TData> data, IEnumerable<PostDetailTransformResult> postDetails);
    }
}
