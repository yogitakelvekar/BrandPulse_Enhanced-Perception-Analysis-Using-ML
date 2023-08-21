using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.ETL.Transform
{
    public class FinalTransformResult
    {
        public string SearchTermId { get; set; } = string.Empty;
        public string SearchTerm { get; set; } = string.Empty;
        public DateTime SearchDateTime { get; set; }

        public List<PostDetailTransformResult> PostDataTransformResult { get; private set; } = new List<PostDetailTransformResult>();

        public List<SentimentTransformResult> SentimentTransformResult { get; private set; } = new List<SentimentTransformResult>();

        public List<WordCloudTransformResult> WordCloudTransformResult { get; private set; } = new List<WordCloudTransformResult>();

        public List<InfluencerTransformResult> InfluencerTransformResult { get; private set; } = new List<InfluencerTransformResult>();

        public void AddPostDataTransformResult(IEnumerable<PostDetailTransformResult> results)
        {
            results.ToList().ForEach(r => r.SearchTermId = SearchTermId);
            PostDataTransformResult.AddRange(results);
        }

        public void AddSentimentTransformResult(IEnumerable<SentimentTransformResult> results)
        {
            SentimentTransformResult.AddRange(results);
        }

        public void AddWordCloudTransformResult(IEnumerable<WordCloudTransformResult> results)
        {
            WordCloudTransformResult.AddRange(results);
        }

        public void AddInfluencerTransformResult(IEnumerable<InfluencerTransformResult> results)
        {
            InfluencerTransformResult.AddRange(results);
        }

    }
}
