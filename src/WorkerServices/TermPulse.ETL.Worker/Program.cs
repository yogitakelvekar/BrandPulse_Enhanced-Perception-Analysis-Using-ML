using TermPulse.Application;
using TermPulse.HttpServices;
using TermPulse.Persistence;
using TermPulse.MessagingBus;
using Serilog;

namespace TermPulse.Transform.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                 .UseSerilog((hostingContext, loggerConfiguration) =>
                 {
                     loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                 })
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