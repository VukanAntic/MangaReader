using System;
using IdentityServer.DTOs;
using IdentityServer.Entities;

namespace IdentityServer.AuthenticationServices
{
    public interface IAuthenticationService
	{
        Task<User?> ValidateUser(UserCredentialsDTO userCredentials);
        Task<AuthenticationModel> CreateAuthenticationModel(User user);
    }
}

