using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _5s.Model
{
    public class Ratings
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public float Sort { get; set; }
        public float SetInOrder { get; set; }
        public float Shine { get; set; }
        public float Standarize { get; set; }
        public float Sustain { get; set; }
        public float Security { get; set; }
        public Boolean isActive { get; set; }
        public DateTime DateModified { get; set; }
        public string SpaceId { get; set; }
    }
}
