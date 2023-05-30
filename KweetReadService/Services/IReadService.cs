using KweetReadService.Models;
using KweetService.RabbitMq;

namespace KweetReadService.Services
{
    public interface IReadService
    {
        public Task Write(KweetDTO dto);
        
    }
}
