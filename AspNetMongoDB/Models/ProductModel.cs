using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AspNetMongoDB.Models
{
    public class ProductModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        [BsonElement("ProductName")]
        public string ProductName { get; set; }
        
        [BsonElement("ProductDescription")]
        public string ProductDescription { get; set; }
        
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
    }
}
