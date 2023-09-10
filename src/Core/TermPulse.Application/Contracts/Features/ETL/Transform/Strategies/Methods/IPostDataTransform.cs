using TermPulse.Application.Models.ETL.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods
{
    public interface IPostDataTransform<TData> where TData : class
    {
        Task<IEnumerable<PostDetailTransformResult>> TransformAsync(IEnumerable<TData> data);
    }
}
