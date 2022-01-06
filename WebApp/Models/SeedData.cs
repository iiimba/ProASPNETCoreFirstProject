using Microsoft.EntityFrameworkCore;
using System;
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
                var o1 = new Order { Number = "SA10854", PurchaseDate = new DateTime(2022, 1, 2, 10, 30, 24), Discount = 15 };
                var o2 = new Order { Number = "SA15846", PurchaseDate = new DateTime(2022, 1, 3, 15, 16, 57), Discount = 10.7m };

                var c1 = new Category { Name = "Watersports" };
                var c2 = new Category { Name = "Soccer" };
                var c3 = new Category { Name = "Chess" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Price = 275,
                        Category = c1,
                        Avaliable = true,
                        Order = o1
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Price = 48.95m,
                        Category = c1,
                        Avaliable = true,
                        Order = o1
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Price = 19.50m,
                        Category = c2,
                        Avaliable = true,
                        Order = o1
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Price = 34.95m,
                        Category = c2,
                        Avaliable = true
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Price = 79500,
                        Category = c2,
                        Avaliable = true,
                        Order = o1
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Price = 16,
                        Category = c3,
                        Avaliable = true,
                        Order = o1
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Price = 29.95m,
                        Category = c3,
                        Avaliable = true
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Price = 75,
                        Category = c3,
                        Avaliable = true,
                        Order = o2
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Price = 1200,
                        Category = c3,
                        Avaliable = true,
                        Order = o2
                    });

                context.SaveChanges();
            }
        }
    }
}
