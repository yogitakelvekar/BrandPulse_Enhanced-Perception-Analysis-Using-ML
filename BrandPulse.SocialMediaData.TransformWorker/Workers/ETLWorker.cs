namespace BrandPulse.Transform.Worker.Workers
{
    public class ETLWorker : BackgroundService
    {
        private readonly ILogger<ETLWorker> _logger;

        public ETLWorker(ILogger<ETLWorker> logger)
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