using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IISTestApplication.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("company")]
        public string Company { get; set; }

        [BsonElement("languages")]
        public string[] Languages { get; set; }
    }
}
