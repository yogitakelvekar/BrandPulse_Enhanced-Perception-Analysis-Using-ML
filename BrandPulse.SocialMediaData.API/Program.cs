
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

            builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("AppSettings"));

            builder.Services.AddSingleton<YouTubeService>();
            builder.Services.AddTransient<YouTubeHttpService>();

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