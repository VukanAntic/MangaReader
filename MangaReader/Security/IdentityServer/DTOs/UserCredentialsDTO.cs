using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs
{
	public class UserCredentialsDTO
	{
        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; } = string.Empty;
    }
}

