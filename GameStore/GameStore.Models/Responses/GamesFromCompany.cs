using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using GameStore.Models.DTO;

namespace GameStore.Models.Responses
{
    public class GamesFromCompany
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("Employees")]
        public int Employees { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
