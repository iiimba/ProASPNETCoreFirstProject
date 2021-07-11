using IISTestApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace IISTestApplication
{
    public static class OrderSeedData
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
                var o1 = new Order { Name = "Order1" };
                var o2 = new Order { Name = "Order1" };

                var p1 = new Product { Name = "Milk", Price = 28.5m };
                var p2 = new Product { Name = "Eggs", Price = 8 };
                var p3 = new Product { Name = "Butter", Price = 42 };
                var p4 = new Product { Name = "Water", Price = 13.5m };
                var p5 = new Product { Name = "Tea", Price = 54.25m };

                var ol1 = new OrderLine { Order = o1, Product = p1, Quantity = 2 };
                var ol2 = new OrderLine { Order = o1, Product = p2, Quantity = 10 };
                var ol3 = new OrderLine { Order = o2, Product = p3, Quantity = 1 };
                var ol4 = new OrderLine { Order = o2, Product = p4, Quantity = 2 };
                var ol5 = new OrderLine { Order = o2, Product = p5, Quantity = 1 };

                context.AddRange(new OrderLine[] { ol1, ol2, ol3, ol4, ol5 });

                context.SaveChanges();
            }
        }
    }
}
