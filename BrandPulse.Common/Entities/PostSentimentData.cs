using BrandPulse.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Domain.Entities
{
    public class PostSentimentData : AuditableEntity
    {
        public Guid Id { get; set; }
        public string SearchTermId { get; set; }
        public string? PostId { get; set; }
        public int PlatformId { get; set; }
        public DateTime PostDate { get; set; }
        public string? PostContent { get; set; }
        public int PostLikes { get; set; }
        public int PostDislikes { get; set; }
    }
}
