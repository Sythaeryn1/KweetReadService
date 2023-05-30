namespace KweetReadService.Data
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string CollectionNameKweet { get; set; }
        public string CollectionNameUser { get; set; }
    }
}
