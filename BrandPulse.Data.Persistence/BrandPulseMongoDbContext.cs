using MongoDB.Driver;

namespace BrandPulse.SocialMediaData.TransformWorker.Data
{
    public class BrandPulseMongoDbContext
    {
        public IMongoDatabase Database { get; }

        public BrandPulseMongoDbContext(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(databaseName);
            CollectionName = collectionName;
        }

        public string CollectionName { get; }
    }
}
