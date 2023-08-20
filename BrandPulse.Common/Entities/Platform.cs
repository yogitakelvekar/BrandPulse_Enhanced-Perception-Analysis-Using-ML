using BrandPulse.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Domain.Entities
{
    public class Platform : AuditableEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string PlatformDefaultUrl { get; set; } = string.Empty;
        public string PlatformIcon { get; set; } = string.Empty;
    }
}
