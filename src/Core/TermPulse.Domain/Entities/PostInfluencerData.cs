using TermPulse.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Domain.Entities
{
    public class PostInfluencerData : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid PostDetailId { get; set; }
        public int PotentialReach { get; set; }
        public int Engagement { get; set; }
    }
}
