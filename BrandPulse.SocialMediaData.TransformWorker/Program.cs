using BrandPulse.Persistence;
using BrandPulse.SocialMediaData.TransformWorker.Data;
using BrandPulse.SocialMediaData.TransformWorker.Settings;
using BrandPulse.SocialMediaData.TransformWorker.Workers;
using Microsoft.EntityFrameworkCore;

namespace BrandPulse.SocialMediaData.TransformWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddPersistenceServices(hostContext.Configuration);
                    services.AddHostedService<SentimentDataTransformerWorker>();
                })
                .Build();

            host.Run();
        }
    }
}