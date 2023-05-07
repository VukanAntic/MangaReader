using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace IdentityServer.Entities
{
    [CollectionName("Roles")]
    public class Role : MongoIdentityRole<Guid>
    {
        public string RoleName { get; set; }
    }
}
