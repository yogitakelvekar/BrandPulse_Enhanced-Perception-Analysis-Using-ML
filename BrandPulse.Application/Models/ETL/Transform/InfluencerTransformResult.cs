using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.ETL.Transform
{
    public class InfluencerTransformResult : TransformResult
    {
        public string AuthorName { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public int PotentialReach { get; set; }
        public int Engagement { get; set; }
        public string? Profile { get; set; }
        public string? Country { get; set; }
    }
}
