using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs
{
    public class UserChangeEmailDTO
    {
        [Required(ErrorMessage = "New email is required")]
        public string NewEmail { get; set; } = string.Empty;
    }
}
