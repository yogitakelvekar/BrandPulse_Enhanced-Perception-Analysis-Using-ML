using BrandPulse.Application.Contracts.Features.ETL;
using BrandPulse.Application.Contracts.Infrastructure.MessagingBus;
using BrandPulse.Application.Features.ETL;
using BrandPulse.Application.Models.Infrastructure.MessagingBus;
using BrandPulse.SocialMediaData.TransformWorker.Data;

namespace BrandPulse.Transform.Worker.Workers
{
    public class ETLWorker : BackgroundService
    {
        private readonly ILogger<ETLWorker> _logger;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IQueueMessagingBus<ETLMessage> _messageBus;

        public ETLWorker(ILogger<ETLWorker> logger, IServiceScopeFactory scopeFactory, IQueueMessagingBus<ETLMessage> messageBus)
        {
            _logger = logger;
            this.scopeFactory = scopeFactory;
            _messageBus = messageBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(async () => await _messageBus.StopProcessingAsync());

            _messageBus.ReceivedMessage(async (etlMessage) =>
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var localETLWorkflowManager = scope.ServiceProvider.GetRequiredService<IETLWorkflowManager>();
                    _logger.LogInformation("Received message at: {time}", DateTimeOffset.Now);
                    var result = await localETLWorkflowManager.Run(etlMessage.SearchTermId); // Assuming your ETLMessage has a SearchTermId
                    Console.WriteLine($"ETL Operation result - {result}");
                }
            });

            // Keeps the service running
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}