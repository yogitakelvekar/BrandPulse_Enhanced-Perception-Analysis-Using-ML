using MongoDB.Driver;

namespace BrandPulse.SocialMediaData.API.Data
{
    public class MongoDbContext
    {
        public IMongoDatabase Database { get; }

        public MongoDbContext(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(databaseName);
            CollectionName = collectionName;
        }

        public string CollectionName { get; }
    }
}
