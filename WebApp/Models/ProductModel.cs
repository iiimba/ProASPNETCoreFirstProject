using System;

namespace WebApp.Models
{
    public class ProductModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public long? CategoryId { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Avaliable { get; set; }
    }
}
