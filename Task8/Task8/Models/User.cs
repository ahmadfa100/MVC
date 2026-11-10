using Microsoft.AspNetCore.Identity;

namespace Task8.Models
{
    public class UserFromIdentity : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
