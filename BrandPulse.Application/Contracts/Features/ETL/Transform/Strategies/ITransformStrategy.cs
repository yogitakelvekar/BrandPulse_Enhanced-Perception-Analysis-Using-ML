using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Contracts.Features.ETL.Transform.Strategies
{
    public interface ITransformStrategy
    {
        Task<FinalTransformResult> TransformAsync();
    }
}
