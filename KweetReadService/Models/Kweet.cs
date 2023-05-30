using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KweetReadService.Models
{
    public class Kweet
    {
        [BsonElement("Id")]
        public long Id { get; set; }
        [BsonElement("Text")]
        public string Text { get; set; }
        [BsonElement("UserId")]
        public long UserId { get; set; }
    }
}
