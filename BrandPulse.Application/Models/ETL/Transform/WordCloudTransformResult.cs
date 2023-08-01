using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.ETL.Transform
{
    public class WordCloudTransformResult : TransformResult
    {
        public string PostId { get; set; }
        public int PlatformId { get; set; }     
        public List<string> Hashtags { get; set; }
        public DateTime PostDate { get; set; }
    }
}
