using BrandPulse.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Domain.Entities
{
    public class PostWordCloudData : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid PostDetailId { get; set; }
        public string? Hashtags { get; set; }
    }
}
