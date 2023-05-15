using System;
using IdentityServer.Entities;

namespace IdentityServer.Repositories.Interfaces
{
	public interface IRefreshTokenRepository
	{
        Task Add(RefreshToken refreshToken);
        Task<bool> Remove(string id);
    }
}

