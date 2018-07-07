using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}