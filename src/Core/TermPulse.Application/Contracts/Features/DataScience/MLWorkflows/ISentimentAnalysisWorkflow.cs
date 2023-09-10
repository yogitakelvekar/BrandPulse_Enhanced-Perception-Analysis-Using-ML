namespace TermPulse.Application.Contracts.Features.DataScience.MLWorkflows
{
    public interface ISentimentAnalysisWorkflow
    {
        Task Run(string searchTermId);
    }
}