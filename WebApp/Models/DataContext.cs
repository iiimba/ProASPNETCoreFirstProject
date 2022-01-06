using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.DateCreated).HasColumnType("datetime2(0)").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Order>().Property(p => p.PurchaseDate).HasColumnType("datetime2(0)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
