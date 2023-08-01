using BrandPulse.Application.Contracts.Features.ETL;
using BrandPulse.Application.Features.ETL;

namespace BrandPulse.Transform.Worker.Workers
{
    public class ETLWorker : BackgroundService
    {
        private readonly ILogger<ETLWorker> _logger;
        private readonly IETLWorkflowManager eTLWorkflowManager;

        public ETLWorker(ILogger<ETLWorker> logger, IETLWorkflowManager eTLWorkflowManager)
        {
            _logger = logger;
            this.eTLWorkflowManager = eTLWorkflowManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await eTLWorkflowManager.Run("64c5733e392e3b23d859c9cb");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}