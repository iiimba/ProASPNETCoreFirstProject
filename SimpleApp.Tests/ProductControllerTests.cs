using Moq;
using ProASPNETCoreFirstProject.Controllers;
using ProASPNETCoreFirstProject.Models;
using System.Collections.Generic;
using Xunit;

namespace SimpleApp.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void IndexActionModelIsComplete()
        {
            // Arrange
            Product[] testData = new Product[]
            {
                new Product { Name = "P1", Price = 75.10M },
                new Product { Name = "P2", Price = 120M },
                new Product { Name = "P3", Price = 110M }
            };

            var mock = new Mock<IDataSource>();
            mock.SetupGet(m => m.Products).Returns(testData);

            var controller = new ProductController();
            controller.dataSource = mock.Object;


            // Act
            var model = controller.Index()?.ViewData.Model as IEnumerable<Product>;

            // Assert
            Assert.Equal(testData, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
            mock.VerifyGet(m => m.Products, Times.Once);
        }
    }
}
