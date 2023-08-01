using BrandPulse.Application.Contracts.Infrastructure.HttpServices;
using BrandPulse.HttpServices.Services;
using BrandPulse.HttpServices.Settings;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrandPulse.HttpServices
{
    public static class HttpServiceRegistration
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
        {
            var settingsSection = configuration.GetSection("HttpServicesSettings");
            var settings = settingsSection.Get<HttpServicesSettings>();

            services.Configure<HttpServicesSettings>(settingsSection);
            services.AddTransient<YouTubeService>(serviceProvider =>
            new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = settings.YouTubeSettings.ApplicationName,
                ApiKey = settings.YouTubeSettings.ApiKey
            }));
            services.AddTransient<IYouTubeHttpService,YouTubeHttpService>();
            services.AddTransient<IRedditHttpService, RedditHttpService>();
            services.AddHttpClient<ITwitterHttpService, TwitterHttpService>(client =>
            {
                client.BaseAddress = new Uri(settings.TwitterSettings.TwitterBaseURL);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", settings.TwitterSettings.XRapidAPIHost);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", settings.TwitterSettings.XRapidAPIKey);
            });
            return services;
        }
    }
}
