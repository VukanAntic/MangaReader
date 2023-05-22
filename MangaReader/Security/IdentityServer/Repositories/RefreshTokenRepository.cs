using System;
using IdentityServer.Repositories.Interfaces;
using IdentityServer.Data.Interfaces;
using IdentityServer.Entities;
using MongoDB.Driver;

namespace IdentityServer.Repositories
{
	public class RefreshTokenRepository : IRefreshTokenRepository
	{
        private readonly IIdentityServerContext _context;

        public RefreshTokenRepository(IIdentityServerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.InsertOneAsync(refreshToken);
        }

        public async Task<bool> Remove(string token)
        {
            var deleteResult = await _context.RefreshTokens.DeleteOneAsync(t => t.Token == token);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
