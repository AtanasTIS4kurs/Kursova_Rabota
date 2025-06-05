﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.Models.Responses
{
    public class GameOrderResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("Name")]
        public string Name { get; set; } = null!;
        [BsonElement("Price")]
        public decimal Price { get; set; }
        [BsonElement("Company")]
        public string Company { get; set; } = null!;
    }
}
