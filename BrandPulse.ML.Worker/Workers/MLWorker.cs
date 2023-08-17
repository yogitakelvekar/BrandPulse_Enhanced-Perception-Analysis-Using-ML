using BrandPulse.Application.Contracts.Features.DataScience;
using BrandPulse.Application.Contracts.Infrastructure.MessagingBus;
using BrandPulse.Application.Models.Infrastructure.MessagingBus;

namespace BrandPulse.ML.Worker
{
    public class MLWorker : BackgroundService
    {
        private readonly ILogger<MLWorker> logger;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IQueueMessagingBus<MLMessage> messageBus;

        public MLWorker(ILogger<MLWorker> logger, IServiceScopeFactory scopeFactory, IQueueMessagingBus<MLMessage> messageBus)
        {
            this.logger = logger;
            this.scopeFactory = scopeFactory;
            this.messageBus = messageBus;        
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("BrandPulse.ML.Worker service listening to Azure Service Bus........");

            stoppingToken.Register(async () => await messageBus.StopProcessingAsync());

            messageBus.ReceivedMessage(RunMLOperation);
            //await RunMLOperation(new MLMessage { SearchTermId = "64dc21d48b42d7784745e6fb" });
            // Keeps the service running
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task RunMLOperation(MLMessage mlMessage)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var workflowManger = scope.ServiceProvider.GetRequiredService<IMLWorkflowManger>();
                logger.LogInformation("Received message at: {time}", DateTimeOffset.Now);
                var result = await workflowManger.Run(mlMessage.SearchTermId);
                Console.WriteLine($"ML Operation result - {result}");
            }
        }
    }
}