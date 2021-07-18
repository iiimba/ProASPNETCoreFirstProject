using IISTestApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace IISTestApplication
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts)
        {

        }

        public DbSet<Person> People { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<FileMetadata> FileMetadatas { get; set; }

        public DbSet<File> Files { get; set; }
    }
}
