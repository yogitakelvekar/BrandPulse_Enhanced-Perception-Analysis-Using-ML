namespace BrandPulse.Application.Contracts.Features.DataScience.DataProcessors
{
    public interface ISentimentDataProcessor
    {
        List<string> PreProcessPostContent(List<string> postContent);
        string PreProcessSingleText(string inputText);
    }
}