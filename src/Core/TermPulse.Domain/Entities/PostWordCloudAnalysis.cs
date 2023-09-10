using TermPulse.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Domain.Entities
{
    public class PostWordCloudAnalysis : AuditableEntity
    {
        public Guid Id { get; set; }
        public string SearchTermId { get; set; } = string.Empty;
        public string Hashtag { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
