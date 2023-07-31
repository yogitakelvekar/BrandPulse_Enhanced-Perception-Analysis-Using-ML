using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.ETL.Transform
{
    public class TransformResult
    {
        public List<SentimentTransformResult> SentimentTransformResult { get; private set; }

        public List<WordCloudTransformResult> WordCloudTransformResult { get; private set; }

        public TransformResult() 
        {
            SentimentTransformResult = new List<SentimentTransformResult>();
            WordCloudTransformResult = new List<WordCloudTransformResult>();
        }

        public void AddSentimentTransformResult(IEnumerable<SentimentTransformResult> results)
        {
            SentimentTransformResult.AddRange(results);
        }

        public void AddWordCloudTransformResult(IEnumerable<WordCloudTransformResult> results)
        {
            WordCloudTransformResult.AddRange(results);
        }
    }
}
