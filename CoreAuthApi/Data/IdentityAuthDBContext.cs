using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace CoreAuthApi.Data
{
    public class IdentityAuthDBContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet <Employee>Employees { get; set; }
        public DbSet <Experience>Experiences { get; set; }

    }

}
