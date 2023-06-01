using KweetService.RabbitMq;
using KweetWriteService.Models;

namespace KweetWriteService.Services
{
    public interface IKweetService
    {
        public Task<KweetDTO> CreateKweet(Kweet kweet);
        public Task<KweetDTO> UpdateKweet(Kweet kweet);
        public Task<bool> DeleteKweet(long id);
    }
}
