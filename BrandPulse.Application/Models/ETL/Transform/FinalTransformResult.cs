using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.ETL.Transform
{
    public class FinalTransformResult
    {
        public List<SentimentTransformResult> SentimentTransformResult { get; private set; }

        public List<WordCloudTransformResult> WordCloudTransformResult { get; private set; }

        public FinalTransformResult() 
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

        public void AddSearchTerm(string searchTermId)
        {
            WordCloudTransformResult.ForEach(result => result.SearchTermId = searchTermId);
            SentimentTransformResult.ForEach(result => result.SearchTermId = searchTermId);
        }
    }
}
