using System;

namespace WebApp.Models
{
    public class ProductVM
    {
        public long ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public CategoryVM Category { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Avaliable { get; set; }
    }
}
