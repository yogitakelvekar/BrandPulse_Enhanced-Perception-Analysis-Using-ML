using BrandPulse.Application.Models.Infrastructure.MessagingBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Contracts.Infrastructure.MessagingBus
{
    public interface IQueueMessagingBus
    {
        Task SendMessageAsync(BaseMessage message);
    }

    public interface IQueueMessagingBus<T> where T : BaseMessage
    {
        Task SendMessageAsync(T message);
        void ReceivedMessage(Func<T, Task> processMessageFunc);
        Task StopProcessingAsync();
    }
}
