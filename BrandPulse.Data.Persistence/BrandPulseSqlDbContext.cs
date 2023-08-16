using BrandPulse.Domain.Entities;
using BrandPulse.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.SocialMediaData.TransformWorker.Data
{
    public class BrandPulseSqlDbContext : DbContext
    {
        public BrandPulseSqlDbContext(DbContextOptions<BrandPulseSqlDbContext> options)
       : base(options)
        {
        }

        public DbSet<PostSentimentData> PostSentimentData { get; set; }
        public DbSet<PostWordCloudData> PostWordCloudData { get; set; }
        public DbSet<PostInfluencerData> PostInfluencerData { get; set; }
        public DbSet<PostSentimentAnalysis> PostSentimentAnalysis { get; set; }
        public DbSet<PostWordCloudAnalysis> PostWordCloudAnalysis { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;              
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;                     
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
