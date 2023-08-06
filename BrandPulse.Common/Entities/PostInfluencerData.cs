using BrandPulse.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Domain.Entities
{
    public class PostInfluencerData : AuditableEntity
    {
        public Guid Id { get; set; }
        public string SearchTermId { get; set; }
        public string PostId { get; set; }
        public int PlatformId { get; set; }
        public DateTime PostDate { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public int PotentialReach { get; set; }
        public int Engagement { get; set; }
        public string? Profile { get; set; }
        public string? Country { get; set; }
    }
}
