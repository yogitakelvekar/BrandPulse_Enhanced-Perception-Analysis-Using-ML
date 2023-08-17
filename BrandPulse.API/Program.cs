using BrandPulse.Application;
using BrandPulse.HttpServices;
using BrandPulse.MessagingBus;
using BrandPulse.ML;
using BrandPulse.ML.Worker;
using BrandPulse.Persistence;
using BrandPulse.Transform.Worker;
using Serilog;

namespace BrandPulse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddHttpServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddAzureServiceBus(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddMLServices(builder.Configuration);
            builder.Services.AddHostedService<ETLWorker>();
            builder.Services.AddHostedService<MLWorker>();

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

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}