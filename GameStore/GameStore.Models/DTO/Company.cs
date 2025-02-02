using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameStore.Models.DTO
{
    public class Company
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("Employees")]
        public int Employees { get; set; }

    }
}
