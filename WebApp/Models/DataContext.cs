using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>().Property(p => p.DateCreated).HasComputedColumnSql("getdate()").ValueGeneratedOnAdd();
        //    modelBuilder.Entity<Product>().Property(p => p.DateUpdated).HasComputedColumnSql("getdate()").ValueGeneratedOnAddOrUpdate();

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
