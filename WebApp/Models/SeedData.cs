using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApp.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if (!context.Products.Any() && !context.Categories.Any())
            {
                var c1 = new Category { Name = "Watersports" };
                var c2 = new Category { Name = "Soccer" };
                var c3 = new Category { Name = "Chess" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Price = 275,
                        Category = c1
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Price = 48.95m,
                        Category = c1
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Price = 19.50m,
                        Category = c2
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Price = 34.95m,
                        Category = c2
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Price = 79500,
                        Category = c2
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Price = 16,
                        Category = c3
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Price = 29.95m,
                        Category = c3
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Price = 75,
                        Category = c3
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Price = 1200,
                        Category = c3
                    });

                context.SaveChanges();
            }
        }
    }
}
