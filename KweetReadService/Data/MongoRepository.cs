using KweetReadService.Models;
using KweetService.RabbitMq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace KweetReadService.Data
{
    public class MongoRepository : IMongoRepository
    { 
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Kweet> _collectionKweet;
        private readonly IMongoCollection<User> _collectionUser;

        public MongoRepository(IMongoDbSettings settings)
        {
            _database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collectionKweet = _database.GetCollection<Kweet>(settings.CollectionNameKweet);
            _collectionUser = _database.GetCollection<User>(settings.CollectionNameUser);
        }
        public async Task<long> DeleteAllKweets(long userId)
        {
            var filter = Builders<Kweet>.Filter.Eq("UserId", userId);
            var result = await _collectionKweet.DeleteManyAsync(filter);
            return result.DeletedCount;
        }

        public async Task<bool> DeleteKweetById(long id)
        {
            var filter = Builders<Kweet>.Filter.Eq("Id", id);
            var result = await _collectionKweet.DeleteOneAsync(filter);
            return result.DeletedCount != 0;
        }

        public async Task<List<Kweet>> GetAllKweets()
        {
            var result = await _collectionKweet.Find(new BsonDocument()).ToListAsync();
            return result;
        }

        public async Task<Kweet> GetKweetById(long id)
        {
            var filter = Builders<Kweet>.Filter.Eq("Id", id);
            var result = await _collectionKweet.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<Kweet>> GetKweetsByUserId(long userId)
        {
            var filter = Builders<Kweet>.Filter.Eq("UserId", userId);
            var result = await _collectionKweet.Find(filter).ToListAsync();
            return result;
        }

        public async Task<User> GetUser(string name)
        {
            var filter = Builders<User>.Filter.Eq("Name", name);
            var result = await _collectionUser.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task InsertKweet(Kweet kweet)
        {
            await _collectionKweet.InsertOneAsync(kweet);

        }

        public async Task<Kweet> UpdateKweet(Kweet kweet)
        {
            var filter = Builders<Kweet>.Filter.Eq("Id", kweet.Id);
            var update = Builders<Kweet>.Update.Set("Text", kweet.Text);

            await _collectionKweet.UpdateOneAsync(filter, update);

            return kweet;
        }
    }
}
