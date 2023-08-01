namespace BrandPulse.Application.Contracts.Features.ETL
{
    public interface IETLWorkflowManager
    {
        Task<bool> Run(string searchId);
    }
}