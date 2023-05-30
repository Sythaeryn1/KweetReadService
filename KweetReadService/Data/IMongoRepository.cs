using KweetReadService.Models;
using KweetService.RabbitMq;

namespace KweetReadService.Data
{
    public interface IMongoRepository
    {
        public Task<List<Kweet>> GetAllKweets();
        public Task<User> GetUser(string name);
        public Task<Kweet> GetKweetById(long id);
        public Task<List<Kweet>> GetKweetsByUserId(long userId);
        public Task InsertKweet(Kweet kweet);
        public Task<Kweet> UpdateKweet(Kweet kweet);
        public Task<bool> DeleteKweetById(long id);
        public Task<long> DeleteAllKweets(long userId);
    }
}
