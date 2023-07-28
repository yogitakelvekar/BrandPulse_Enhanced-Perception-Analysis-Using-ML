using BrandPulse.Domain.Collections;
using BrandPulse.SocialMediaData.TransformWorker.Data;
using MongoDB.Driver;

namespace BrandPulse.Persistence.Repositories
{
    public class SocialMediaAggregateRepository
    {
        private readonly IMongoCollection<SocialMediaAggregates> _collection;

        public SocialMediaAggregateRepository(BrandPulseMongoDbContext context)
        {
            _collection = context.Database.GetCollection<SocialMediaAggregates>(context.CollectionName);
        }

        public async Task StoreDataAsync(SocialMediaAggregates data)
        {
            await _collection.InsertOneAsync(data);
        }

        public async Task<SocialMediaAggregates> GetDataAsync(string searchTermId)
        {
            var result = await _collection.FindAsync(a => a.Id == searchTermId);
            return result.Single();
        }
    }
}
