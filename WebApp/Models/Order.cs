using System;

namespace WebApp.Models
{
    public class Order
    {
        public long OrderId { get; set; }

        public string Number { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal Discount { get; set; }
    }
}
