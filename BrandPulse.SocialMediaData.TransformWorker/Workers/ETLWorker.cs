using BrandPulse.Application.Contracts.Features.ETL;
using BrandPulse.Application.Features.ETL;
using BrandPulse.SocialMediaData.TransformWorker.Data;

namespace BrandPulse.Transform.Worker.Workers
{
    public class ETLWorker : BackgroundService
    {
        private readonly ILogger<ETLWorker> _logger;
        private readonly IServiceScopeFactory scopeFactory;

        public ETLWorker(ILogger<ETLWorker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;         
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var localETLWorkflowManager = scope.ServiceProvider.GetRequiredService<IETLWorkflowManager>();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var result = await localETLWorkflowManager.Run("64c5733e392e3b23d859c9cb");
                Console.WriteLine($"ETL Operation result - {result}");
            }
        }
    }
}