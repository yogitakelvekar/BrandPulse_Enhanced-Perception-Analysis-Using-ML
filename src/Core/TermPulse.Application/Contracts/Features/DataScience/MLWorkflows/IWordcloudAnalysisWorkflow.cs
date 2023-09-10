namespace TermPulse.Application.Contracts.Features.DataScience.MLWorkflows
{
    public interface IWordcloudAnalysisWorkflow
    {
        Task Run(string searchTermId);
    }
}