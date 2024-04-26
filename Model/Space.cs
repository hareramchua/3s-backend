using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _5s.Model
{
    public class Space
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public List<SpaceImage>? Pictures { get; set; }
        public string RoomId { get; set; }
        public string? Standard {get;set; }
        public DateTime? ViewedDate {get;set; }
        public DateTime? AssessedDate {get;set; }
    }
}
