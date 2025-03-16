using Microsoft.AspNetCore.Identity;

namespace test2.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Age { get; set; }
    }
}
