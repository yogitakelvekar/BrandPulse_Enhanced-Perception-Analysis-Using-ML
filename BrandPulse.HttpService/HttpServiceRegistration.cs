using BrandPulse.HttpService.Settings;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.HttpService
{
    public static class HttpServiceRegistration
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
        {
            var settingsSection = configuration.GetSection("AppSettings");
            var settings = settingsSection.Get<ApplicationSettings>();

            services.Configure<ApplicationSettings>(settingsSection);
            services.AddSingleton<YouTubeService>();
            services.AddScoped<YouTubeHttpService>();
            services.AddScoped<RedditHttpService>();
            services.AddHttpClient<TwitterHttpService>(client =>
            {
                client.BaseAddress = new Uri(settings.TwitterSettings.TwitterBaseURL);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", settings.TwitterSettings.XRapidAPIHost);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", settings.TwitterSettings.XRapidAPIKey);
            });
            return services;
        }
    }
}
