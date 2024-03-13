using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _5s.Model
{
    public class RedTag
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? ItemName { get; set; }
        public int Quantity { get; set; }
        public string RoomId { get; set; }
    }
}
