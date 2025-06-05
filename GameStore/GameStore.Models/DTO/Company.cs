using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MessagePack;

namespace GameStore.Models.DTO
{
    [MessagePackObject]
    public class Company : ICacheItem<string>
    {
        
        [Key(0)]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("Name")]
        [Key(1)]
        public string Name { get; set; } = null!;

        [BsonElement("Employees")]
        [Key(2)]
        public int Employees { get; set; }
        [BsonElement("DateInserted")]
        [Key(3)]
        public DateTime DateInserted { get; set; }

        public string GetKey()
        {
            return Id;
        }

    }
}
