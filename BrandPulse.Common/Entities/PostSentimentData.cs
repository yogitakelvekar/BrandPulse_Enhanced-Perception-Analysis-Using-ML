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
        public Guid PostDetailId { get; set; }
        public string? SubPostId { get; set; }
        public string? PostContent { get; set; }
        public int PostLikes { get; set; }
        public int PostDislikes { get; set; }
        public DateTime SubPostDate { get; set; }
       
    }
}
