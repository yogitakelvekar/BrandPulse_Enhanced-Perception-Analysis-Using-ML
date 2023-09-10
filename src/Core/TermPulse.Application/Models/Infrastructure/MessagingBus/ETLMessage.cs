using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Models.Infrastructure.MessagingBus
{
    public class ETLMessage : BaseMessage
    {
        public string SearchTermId { get; set; }
    }
}
