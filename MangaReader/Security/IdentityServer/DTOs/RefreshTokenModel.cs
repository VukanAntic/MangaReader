using System;
namespace IdentityServer.DTOs
{
	public class RefreshTokenModel
	{
        public string UserName { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}

