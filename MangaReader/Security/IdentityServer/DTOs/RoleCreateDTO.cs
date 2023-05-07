using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs
{
    public class RoleCreateDTO
    {
        [Required]
        public string RoleName { get; set; }
    }
}
