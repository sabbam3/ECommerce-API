using Microsoft.AspNetCore.Identity;

namespace ECommerce_API.Models.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        public List<ProductEntity>? OwnedProducts { get; set; }
        public UserEntity()
        {
            OwnedProducts = new List<ProductEntity>();
        }
    }
}
