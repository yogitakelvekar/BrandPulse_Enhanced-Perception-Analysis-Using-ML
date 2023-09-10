using TermPulse.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Domain.Entities
{
    public class PostSentimentAnalysis : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid SentimentDataId { get; set; }
        public string? CleanedPostContent { get; set;}
        public string? Sentiment { get; set; }
    }
}
