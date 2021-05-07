using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task Can_Use_Repository()
        {
            // Arrange
            var mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.GetProductsAsync()).ReturnsAsync(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"}
            });

            var controller = new HomeController(mock.Object);
            // Act
            var result = (await controller.Index()).ViewData.Model as Product[];

            // Assert
            Assert.True(result.Length == 2);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
        }
    }
}
