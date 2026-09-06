using Microsoft.AspNetCore.Identity;

namespace CoreAuthApi.Data
{
    public class ApplicationUser: IdentityUser 
    {
        public string? Name { get; set; }
    }
}
