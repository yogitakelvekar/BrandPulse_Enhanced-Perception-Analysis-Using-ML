using TermPulse.Application;
using TermPulse.Persistence;
using TermPulse.MessagingBus;
using TermPulse.HttpServices;
using Serilog;

namespace TermPulse.ML.Worker
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