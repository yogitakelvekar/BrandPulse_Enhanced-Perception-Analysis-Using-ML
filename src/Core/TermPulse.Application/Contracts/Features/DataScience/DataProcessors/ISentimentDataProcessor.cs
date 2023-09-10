namespace TermPulse.Application.Contracts.Features.DataScience.DataProcessors
{
    public interface ISentimentDataProcessor
    {
        string PreProcessSingleText(string inputText);
    }
}