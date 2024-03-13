using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _5s.Model
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Sort { get; set; }
        public string SetInOrder { get; set; }
        public string Shine { get; set; }
        public string Standarize { get; set; }
        public string Sustain { get; set; }
        public string Security { get; set; }
        public Boolean isActive { get; set; }
        public DateTime DateModified { get; set; }
        public string RatingId { get; set; }
    }
}
