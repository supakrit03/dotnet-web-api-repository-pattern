
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RepositoryPattern.Stock.Models
{
	public class StockModel
	{
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

		[BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("index")]
        public string Index { get; set; }

    }
}

