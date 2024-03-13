﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _5s.Model
{
    public class Building
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? BuildingName { get; set; }
        public string? BuildingCode { get; set; }
        public byte[]? Image { get; set; }
    }
}
