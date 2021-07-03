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
    }
}
