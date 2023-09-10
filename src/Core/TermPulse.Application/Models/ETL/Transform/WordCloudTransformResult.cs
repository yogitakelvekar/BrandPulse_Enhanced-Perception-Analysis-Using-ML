using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Models.ETL.Transform
{
    public class WordCloudTransformResult : TransformResult
    {
        public List<string> Hashtags { get; set; } = new List<string>();
    }
}
