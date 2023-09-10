using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods
{
    public interface IWordCloudDataTransform<TData> where TData : class
    {
        Task<IEnumerable<WordCloudTransformResult>> TransformAsync(IEnumerable<TData> data, IEnumerable<PostDetailTransformResult> postDetails);
    }
}
