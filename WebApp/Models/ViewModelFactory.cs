using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public static class ViewModelFactory
    {
        public static ProductViewModel Details(Product p)
        {
            return new ProductViewModel
            {
                Product = p,
                Action = "Details",
                ReadOnly = true,
                Theme = "info",
                ShowAction = false,
                Categories = p == null ? Enumerable.Empty<Category>() : new List<Category> { p.Category }
            };
        }

        public static ProductViewModel Create(Product product, IEnumerable<Category> categories)
        {
            return new ProductViewModel
            {
                Product = product,
                Categories = categories
            };
        }

        public static ProductViewModel Edit(Product product, IEnumerable<Category> categories)
        {
            return new ProductViewModel
            {
                Product = product,
                Categories = categories,
                Theme = "warning",
                Action = "Edit"
            };
        }

        public static ProductViewModel Delete(Product p, IEnumerable<Category> categories)
        {
            return new ProductViewModel
            {
                Product = p,
                Action = "Delete",
                ReadOnly = true,
                Theme = "danger",
                Categories = categories
            };
        }
    }
}