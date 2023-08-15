namespace BrandPulse.Application.Contracts.Features.DataScience
{
    public interface IMLWorkflowManger
    {
        Task<bool> Run(string searchTermId);
    }
}