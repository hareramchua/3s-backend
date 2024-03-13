using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _5s.Model
{
    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int BuildingId { get; set; }
        public string? RoomNumber { get; set; }
        public byte[]? Image { get; set; }
        public string Status { get; set; }
    }
}
