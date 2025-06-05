using MessagePack;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.Models.DTO
{
    [BsonIgnoreExtraElements]
    [MessagePackObject]
    public class Game : ICacheItem<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Key(0)]
        public string Id { get; set; } = null!;
        [BsonElement("Name")]
        [Key(1)]
        public string Name { get; set; } = null!;
        [BsonElement("Price")]
        [Key(2)]
        public decimal Price { get; set; }
        [BsonElement("Company")]
        [Key(3)]
        public string Company { get; set; } = null!;
        [Key(4)]
        public DateTime DateInserted { get; set; }
        public string GetKey()
        {
            return Id;
        }
    }
}
