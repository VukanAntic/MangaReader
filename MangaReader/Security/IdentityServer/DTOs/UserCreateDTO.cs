﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ErrorMessage is required")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
