using BrandPulse.Persistence.Settings;
using BrandPulse.SocialMediaData.TransformWorker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDBSetting = configuration.GetRequiredSection("MongoDBSettings").Get<MongoDBSettings>();
            services.AddSingleton(sp => new BrandPulseMongoDbContext(mongoDBSetting.ConnectionString, mongoDBSetting.Database, mongoDBSetting.Collection));
            services.AddDbContext<BrandPulseSqlDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("BrandPulseSQL")));
            return services;
        }
    }
}
