
using BrandPulse.SocialMediaData.API.Data;
using BrandPulse.SocialMediaData.API.Data.Repositories;
using BrandPulse.SocialMediaData.API.Services.FeatureServices;
using BrandPulse.SocialMediaData.API.Services.HttpServices;
using BrandPulse.SocialMediaData.API.Settings;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;


namespace BrandPulse.SocialMediaData.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var settingsSection = builder.Configuration.GetSection("AppSettings");
            var settings = settingsSection.Get<ApplicationSettings>();

            builder.Services.Configure<ApplicationSettings>(settingsSection);
            builder.Services.AddSingleton(sp => new MongoDbContext(settings.MongoDBSettings.ConnectionString, settings.MongoDBSettings.Database, settings.MongoDBSettings.Collection));
            builder.Services.AddSingleton<YouTubeService>();
            builder.Services.AddScoped<YouTubeHttpService>(); 
            builder.Services.AddScoped<RedditHttpService>();
            builder.Services.AddHttpClient<TwitterHttpService>(client =>
            {
                client.BaseAddress = new Uri(settings.TwitterSettings.TwitterBaseURL);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", settings.TwitterSettings.XRapidAPIHost);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", settings.TwitterSettings.XRapidAPIKey); 
            });
            builder.Services.AddScoped<SocialMediaAggregateRepository>();
            builder.Services.AddScoped<SocialMediaAggregateService>();
           
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}