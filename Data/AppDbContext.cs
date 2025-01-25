using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<ProductInformation> ProductInformation { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Sales> Sales { get; set; }

        public DbSet<SalesRetrive> SalesRetrive { get; set; }
    }
}
