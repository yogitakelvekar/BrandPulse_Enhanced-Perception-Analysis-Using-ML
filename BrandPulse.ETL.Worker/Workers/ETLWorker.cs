using BrandPulse.Application.Contracts.Features.ETL;
using BrandPulse.Application.Contracts.Infrastructure.MessagingBus;
using BrandPulse.Application.Models.Infrastructure.MessagingBus;

namespace BrandPulse.Transform.Worker
{
    public class ETLWorker : BackgroundService
    {
        private readonly ILogger<ETLWorker> _logger;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IQueueMessagingBus<ETLMessage> etlMessageBus;
        private readonly IQueueMessagingBus<MLMessage> mlMessageBus;

        public ETLWorker(ILogger<ETLWorker> logger, IServiceScopeFactory scopeFactory, 
            IQueueMessagingBus<ETLMessage> etlMessageBus, IQueueMessagingBus<MLMessage> mlMessageBus)
        {
            _logger = logger;
            this.scopeFactory = scopeFactory;
            this.etlMessageBus = etlMessageBus;
            this.mlMessageBus = mlMessageBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(async () => await etlMessageBus.StopProcessingAsync());

            etlMessageBus.ReceivedMessage(RunETLOperation);

            await RunETLOperation(new ETLMessage { SearchTermId = "64dc2bdf7c6eb949ee69d1ac" });

            // Keeps the service running
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task RunETLOperation(ETLMessage etlMessage)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var localETLWorkflowManager = scope.ServiceProvider.GetRequiredService<IETLWorkflowManager>();
                _logger.LogInformation("Received message at: {time}", DateTimeOffset.Now);
                var result = await localETLWorkflowManager.Run(etlMessage.SearchTermId);
                Console.WriteLine($"ETL Operation result - {result}");
                if (result)
                {
                    await mlMessageBus.SendMessageAsync(new MLMessage { SearchTermId = etlMessage.SearchTermId });
                    Console.WriteLine($"Message sent to ML Worker");
                }
               
            }
        }
    }
}