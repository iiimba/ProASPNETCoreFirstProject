using ProASPNETCoreFirstProject.Models;
using Xunit;

namespace SimpleApp.Tests
{
    public class ProductTests
    {
        [Fact]
        public void CanChangeProductName()
        {
            // Arrange
            var p = new Product { Name = "Test", Price = 100.0m };

            // Act

            p.Name = "New name";

            // Assert

            Assert.Equal("New name", p.Name);
        }

        [Fact]
        public void CanChangeProductPrice()
        {
            // Arrange
            var p = new Product { Name = "Test", Price = 100.0m };

            // Act
            p.Price = 200.0m;

            //Assert
            Assert.Equal(200.0m, p.Price);
        }
    }
}
