namespace KweetReadService.Data
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string CollectionNameKweet { get; set; }
        string CollectionNameUser { get; set; }
    }
}
