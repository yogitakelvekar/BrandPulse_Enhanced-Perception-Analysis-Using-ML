using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Contracts.Features.ETL.Transform.Strategies
{
    public interface ITransformStrategy
    {
        Task<FinalTransformResult> TransformAsync();
    }
}
