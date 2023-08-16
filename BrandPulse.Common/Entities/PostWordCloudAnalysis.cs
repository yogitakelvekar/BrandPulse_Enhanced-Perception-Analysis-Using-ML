using BrandPulse.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Domain.Entities
{
    public class PostWordCloudAnalysis : AuditableEntity
    {
        public Guid Id { get; set; }
        public string SearchTermId { get; set; }
        public string Hashtag { get; set; }
        public int Count { get; set; }
    }
}
