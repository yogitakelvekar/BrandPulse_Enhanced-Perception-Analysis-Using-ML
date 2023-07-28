using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.SocialMediaData.TransformWorker.Data
{
    public class BrandPulseSqlDbContext : DbContext
    {
        public BrandPulseSqlDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
