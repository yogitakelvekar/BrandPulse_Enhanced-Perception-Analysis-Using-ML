using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Models.Infrastructure.MessagingBus
{
    public class MLMessage : BaseMessage
    {
        public string SearchTermId { get; set; }
    }

}
