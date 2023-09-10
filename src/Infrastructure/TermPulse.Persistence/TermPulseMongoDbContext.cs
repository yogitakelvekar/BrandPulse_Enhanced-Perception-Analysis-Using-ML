using MongoDB.Driver;

namespace TermPulse.SocialMediaData.TransformWorker.Data
{
    public class TermPulseMongoDbContext
    {
        public IMongoDatabase Database { get; }

        public TermPulseMongoDbContext(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(databaseName);
            CollectionName = collectionName;
        }

        public string CollectionName { get; }
    }
}
