using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OData.Models;
using System;
using System.Linq;

namespace OData
{
    public static class CustomerSeedData
    {
        public static void PopulateOrders(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Orders.Any())
            {
                var o1 = new Order { OrderDate = DateTime.Now, Product = "Milk", Quantity = 1, Revenue = 31 };
                var o2 = new Order { OrderDate = DateTime.Now, Product = "Eggs", Quantity = 10, Revenue = 20 };
                var o3 = new Order { OrderDate = DateTime.Now, Product = "Butter", Quantity = 2, Revenue = 54 };
                var o4 = new Order { OrderDate = DateTime.Now, Product = "Water", Quantity = 1, Revenue = 13 };
                var o5 = new Order { OrderDate = DateTime.Now, Product = "Tea", Quantity = 3, Revenue = 52 };

                var c1 = new Customer { CustomerName = "Vlad", CountryId = "Ukraine", Orders = new Order[] { o1, o2, o3 } };
                var c2 = new Customer { CustomerName = "Igor", CountryId = "Poland", Orders = new Order[] { o4, o5 } };

                context.Customers.AddRange(new Customer[] { c1, c2 });

                context.SaveChanges();
            }
        }
    }
}
