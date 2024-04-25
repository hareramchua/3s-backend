﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _5s.Model
{
    public class SpaceImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SpaceId { get; set; } 
        public byte[]? Image { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ForType {get;set;}
        public string? Prediction {get;set;}
    }
}
