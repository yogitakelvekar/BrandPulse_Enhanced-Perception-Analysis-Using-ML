using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.ETL.Transform
{
    public class InfluencerTransformResult : TransformResult
    {
        public int PotentialReach { get; set; }
        public int Engagement { get; set; }
    }
}
