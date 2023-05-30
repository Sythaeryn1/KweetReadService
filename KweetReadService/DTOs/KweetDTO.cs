namespace KweetService.RabbitMq
{
    public class KweetDTO
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long UserId { get; set; }
        public EventTypes EventType { get; set; }
    }
}
