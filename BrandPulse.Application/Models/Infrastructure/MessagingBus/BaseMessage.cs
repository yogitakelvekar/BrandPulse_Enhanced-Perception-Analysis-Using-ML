using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.Infrastructure.MessagingBus
{
    public class BaseMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationDateTime { get; set; } = DateTime.Now;
    }
}
