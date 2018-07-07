using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class RunEfCore
    {
        private readonly NorthwindContext _northwindContext;

        public RunEfCore()
        {
            var conneString    = ConfigReader.GetConnectionString("EfCore");
            var optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>();
            optionsBuilder.UseSqlServer(conneString);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            _northwindContext = new NorthwindContext(optionsBuilder.Options);
        }

        public IEnumerable<Customer> Get()
        {
            return _northwindContext.Customers;
        }
    }
}