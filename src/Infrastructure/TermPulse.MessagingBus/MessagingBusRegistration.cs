using Azure.Messaging.ServiceBus;
using TermPulse.Application.Contracts.Infrastructure.MessagingBus;
using TermPulse.Application.Models.Infrastructure.MessagingBus;
using TermPulse.MessagingBus.Queues;
using TermPulse.MessagingBus.Settings;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.MessagingBus
{
    public static class MessagingBusRegistration
    {
        public static IServiceCollection AddAzureServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            var settingsSection = configuration.GetSection("AzureServiceBusSettings");
            var settings = settingsSection.Get<AzureServiceBusSettings>();

            services.Configure<AzureServiceBusSettings>(settingsSection);

            // Register Service Bus Client for sending messages
            services.AddSingleton(provider => new ServiceBusClient(settings.ConnectionString));
            services.AddSingleton<IQueueMessagingBus<ETLMessage>, AzServiceBusETLQueue>();
            services.AddSingleton<IQueueMessagingBus<MLMessage>, AzServiceBusMLQueue>();

            return services;
        }
    }
}
