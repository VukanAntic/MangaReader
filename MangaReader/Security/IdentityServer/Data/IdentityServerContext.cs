using System;
using MongoDB.Driver;
using IdentityServer.Data.Interfaces;
using IdentityServer.Entities;


namespace IdentityServer.Data
{
	public class IdentityServerContext : IIdentityServerContext
	{
        public IMongoCollection<RefreshToken> RefreshTokens { get; }

        public IdentityServerContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase("IdentityAuthDb");

            RefreshTokens = database.GetCollection<RefreshToken>("RefreshTokens");
            //IdentityServerContextSeed.SeedData(RefreshTokens); mozemo ovo nekad kasnije da odradimo
        }

    }
}
