using Azure.Messaging.ServiceBus;
using BrandPulse.Application.Contracts.Infrastructure.MessagingBus;
using BrandPulse.Application.Models.Infrastructure.MessagingBus;
using Microsoft.Extensions.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrandPulse.MessagingBus.Queues
{
    public class AzServiceBusETLQueue : IQueueMessagingBus<ETLMessage>
    {      
        private readonly ServiceBusSender serviceBusSender;
        private readonly ServiceBusProcessor serviceBusProcessor;

        public AzServiceBusETLQueue(ServiceBusClient serviceBusClient)
        {
            serviceBusSender = serviceBusClient.CreateSender("brandpulse-etl-queue");
            serviceBusProcessor = serviceBusClient.CreateProcessor("brandpulse-etl-queue", new ServiceBusProcessorOptions());
        }

        public async Task SendMessageAsync(ETLMessage messageReceived)
        {
            var eventName = messageReceived.GetType().Name;
            var jsonMessage = JsonSerializer.Serialize(messageReceived);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var message = new ServiceBusMessage(body)
            {
                CorrelationId = Guid.NewGuid().ToString(),
                Subject = eventName,
            };

            await serviceBusSender.SendMessageAsync(message);
        }

        public void ReceivedMessage(Func<ETLMessage, Task> processMessageFunc)
        {
            serviceBusProcessor.ProcessMessageAsync += async args =>
            {
                var body = args.Message.Body.ToString();
                var message = JsonSerializer.Deserialize<ETLMessage>(body);

                if (message != null)
                {
                    await processMessageFunc(message);
                }

                await args.CompleteMessageAsync(args.Message);
            };

            serviceBusProcessor.ProcessErrorAsync += ErrorHandler;
            serviceBusProcessor.StartProcessingAsync();
        }


        public async Task StopProcessingAsync()
        {
            await serviceBusProcessor.StopProcessingAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            // Handle the error here (logging, etc.)
            Console.WriteLine($"Message handler encountered an exception {args.Exception}.");
            return Task.CompletedTask;
        }
    }
}
