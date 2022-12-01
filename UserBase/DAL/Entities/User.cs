using Microsoft.AspNetCore.Identity;

namespace UserBase.DAL.Entities
{
    public class User : IdentityUser
    {
        public byte[]? Image { get; set; }
    }
}
