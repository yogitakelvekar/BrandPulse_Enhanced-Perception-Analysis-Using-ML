using BrandPulse.Application;
using BrandPulse.Persistence;
using BrandPulse.MessagingBus;
using BrandPulse.HttpServices;

namespace BrandPulse.ML.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMLServices(hostContext.Configuration);
                    services.AddApplicationServices(hostContext.Configuration);
                    services.AddHttpServices(hostContext.Configuration);
                    services.AddPersistenceServices(hostContext.Configuration);
                    services.AddAzureServiceBus(hostContext.Configuration);               
                    services.AddHostedService<MLWorker>();
                })
                .Build();

            host.Run();
        }
    }
}