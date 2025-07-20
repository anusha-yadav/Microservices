using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

namespace PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Platform> Platforms { get; set; } 
    }
}
