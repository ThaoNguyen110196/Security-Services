using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace Redislibrary.Services
{
    public class ImageMetadata
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } // Sử dụng string để ánh xạ với ObjectId của MongoDB

        [BsonElement("FileName")]
        public string FileName { get; set; }

        [BsonElement("Url")]
        public string Url { get; set; }

        [BsonElement("Size")]
        public long Size { get; set; }
    }
}