using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.ETL.Transform
{
    public class SentimentTransformResult : TransformResult
    {
        public string PostContent { get; set; } = string.Empty;
        public int PostLikes { get; set; }
        public int PostDislikes { get; set; }
    }
}
