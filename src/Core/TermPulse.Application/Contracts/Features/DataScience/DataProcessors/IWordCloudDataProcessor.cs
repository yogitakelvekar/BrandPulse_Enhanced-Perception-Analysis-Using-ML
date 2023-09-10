using TermPulse.Domain.Entities;

namespace TermPulse.ML.Source.DataProcessors
{
    public interface IWordCloudDataProcessor
    {
        Dictionary<string, int> ExtractAndCountHashtags(IEnumerable<PostWordCloudData> data);
    }
}