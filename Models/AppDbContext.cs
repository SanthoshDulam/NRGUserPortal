using Microsoft.EntityFrameworkCore;

namespace NRGUserPortal.Models
{
    // App DB context - EF Core
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
