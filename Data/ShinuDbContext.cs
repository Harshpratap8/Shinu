using Microsoft.EntityFrameworkCore;
using ModernMeterAPI.Domain;

namespace ModernMeterAPI.Data
{
    public class ShinuDbContext : DbContext
    {
        public ShinuDbContext(DbContextOptions<ShinuDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}