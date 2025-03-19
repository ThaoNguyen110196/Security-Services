using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using StackExchange.Redis;

namespace Redislibrary.Services
{
    public class RedisService : ImageMetadataService
    {
        private readonly IMongoCollection<ImageMetadata> _imageMetadataCollection;
        public RedisService(IMongoDatabase database)
        {
            _imageMetadataCollection = database.GetCollection<ImageMetadata>("ImageMetadata");
        }
        public async Task<ImageMetadata> GetImageMetadataAsync(string id)
        {
            var filter = Builders<ImageMetadata>.Filter.Eq(m => m.Id, new MongoDB.Bson.ObjectId(id));
            return await _imageMetadataCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task InsertImageMetadataAsync(ImageMetadata metadata)
        {
            await _imageMetadataCollection.InsertOneAsync(metadata);
        }
    }
}
