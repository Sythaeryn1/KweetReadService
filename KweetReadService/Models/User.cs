using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KweetReadService.Models
{
    public class User
    {
        [BsonElement("Id")]
        public long Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
