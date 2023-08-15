using BrandPulse.Application;
using BrandPulse.Persistence;
using BrandPulse.MessagingBus;

namespace BrandPulse.ML.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplicationServices(hostContext.Configuration);
                    services.AddPersistenceServices(hostContext.Configuration);
                    services.AddAzureServiceBus(hostContext.Configuration);               
                    services.AddHostedService<MLWorker>();
                })
                .Build();

            host.Run();
        }
    }
}