using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameStore.Models.Requests
{
    public class AddGameRequest
    {
         [BsonElement("Name")]
         public string Name { get; set; } = null!;
         [BsonElement("Price")]
         public decimal Price { get; set; }
         [BsonElement("Company")]
         public string Company { get; set; } = null!;
         [BsonElement("DateInserted")]
         public DateTime DateInserted { get; set; }


    }
}
