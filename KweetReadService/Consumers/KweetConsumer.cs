using MassTransit;
using KweetService.RabbitMq;
using KweetReadService.Services;

namespace KweetReadService.Consumers
{
    public class KweetConsumer : IConsumer<KweetDTO>
    {
        private readonly IReadService _readService;
        public KweetConsumer(IReadService readService)
        {
            _readService = readService;
        }
        public async Task Consume(ConsumeContext<KweetDTO> context)
        {
            KweetDTO dto = new KweetDTO
            {
                Id = context.Message.Id,
                Text = context.Message.Text,
                UserId = context.Message.UserId,
                EventType = context.Message.EventType
            };

            await _readService.Write(dto);
        }
    }
}
