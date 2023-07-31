using BrandPulse.Persistence;
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
                    services.AddPersistenceServices(hostContext.Configuration);
                    services.AddHostedService<ETLWorker>();
                })
                .Build();

            host.Run();
        }
    }
}