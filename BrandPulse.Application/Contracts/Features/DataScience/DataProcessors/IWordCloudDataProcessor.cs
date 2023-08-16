using BrandPulse.Domain.Entities;

namespace BrandPulse.ML.Source.DataProcessors
{
    public interface IWordCloudDataProcessor
    {
        Dictionary<string, int> ExtractAndCountHashtags(IEnumerable<PostWordCloudData> data);
    }
}