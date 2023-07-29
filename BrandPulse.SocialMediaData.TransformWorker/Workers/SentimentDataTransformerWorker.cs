namespace BrandPulse.Transform.Worker.Workers
{
    public class SentimentDataTransformerWorker : BackgroundService
    {
        private readonly ILogger<SentimentDataTransformerWorker> _logger;

        public SentimentDataTransformerWorker(ILogger<SentimentDataTransformerWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}