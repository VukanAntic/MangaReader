using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs
{
    public class RoleCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
