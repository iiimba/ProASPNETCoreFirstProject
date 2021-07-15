using System.Collections.Generic;

namespace OData.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CountryId { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
