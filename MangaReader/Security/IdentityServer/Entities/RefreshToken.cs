using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace IdentityServer.Entities
{
    public class RefreshToken
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Token { get; set; } = String.Empty;
        public DateTime ExpiryTime { get; set; }
    }
}
