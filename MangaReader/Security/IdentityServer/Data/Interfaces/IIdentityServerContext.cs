using System;
using IdentityServer.Entities;
using MongoDB.Driver;


namespace IdentityServer.Data.Interfaces
{
	public interface IIdentityServerContext
	{
        IMongoCollection<RefreshToken> RefreshTokens { get; }
    }
}

