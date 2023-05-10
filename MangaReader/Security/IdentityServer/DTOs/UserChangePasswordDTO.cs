using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs
{
    public class UserChangePasswordDTO
    {
        [Required(ErrorMessage = "Old password is required")]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
