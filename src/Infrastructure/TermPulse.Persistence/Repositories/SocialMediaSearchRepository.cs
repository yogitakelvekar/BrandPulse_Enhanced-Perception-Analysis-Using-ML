using TermPulse.Application.Contracts.Infrastructure.Persistence;
using TermPulse.Domain.Collections;
using TermPulse.SocialMediaData.TransformWorker.Data;
using MongoDB.Driver;

namespace TermPulse.Persistence.Repositories
{
    public class SocialMediaAggregateRepository : ISocialMediaAggregateRepository
    {
        private readonly IMongoCollection<SocialMediaAggregates> _collection;

        public SocialMediaAggregateRepository(TermPulseMongoDbContext context)
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
