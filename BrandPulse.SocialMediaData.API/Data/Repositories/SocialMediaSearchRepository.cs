using BrandPulse.SocialMediaData.API.Models.Entities;
using MongoDB.Driver;

namespace BrandPulse.SocialMediaData.API.Data.Repositories
{
    public class SocialMediaAggregateRepository
    {
        private readonly IMongoCollection<SocialMediaAggregates> _collection;

        public SocialMediaAggregateRepository(MongoDbContext context)
        {
            _collection = context.Database.GetCollection<SocialMediaAggregates>(context.CollectionName);
        }

        public async Task StoreDataAsync(SocialMediaAggregates data)
        {
            await _collection.InsertOneAsync(data);
        }

        // other repository methods go here as needed
    }
}
