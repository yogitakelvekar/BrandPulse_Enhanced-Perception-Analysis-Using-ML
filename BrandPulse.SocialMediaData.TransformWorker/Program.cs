using BrandPulse.Application;
using BrandPulse.HttpServices;
using BrandPulse.Persistence;
using BrandPulse.MessagingBus;
using BrandPulse.Transform.Worker.Workers;

namespace BrandPulse.Transform.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplicationServices(hostContext.Configuration);
                    services.AddHttpServices(hostContext.Configuration);
                    services.AddPersistenceServices(hostContext.Configuration);
                    services.AddAzureServiceBus(hostContext.Configuration);
                    services.AddHostedService<ETLWorker>();
                })
                .Build();

            host.Run();
        }
    }
}