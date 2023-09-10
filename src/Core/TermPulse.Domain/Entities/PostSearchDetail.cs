using TermPulse.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Domain.Entities
{
    public class PostSearchDetail : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SearchTermId { get; set; } = string.Empty;
        public string SearchTerm { get; set; } = string.Empty;
        public DateTime SearchDateTime { get; set; }
    }
}
