using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Application.Commands.AddNewProduct;
using System;
using System.Threading.Tasks;
using Domain.Entities;
using System.Threading;
using Application.Events;
using Microsoft.Extensions.Logging;
using Test.Doubles;

namespace Test.NotificationHandlers
{
    [TestClass]
    public class TestProductCreatedHandler
    {
        MockLogger<ProductCreatedHandler> _logger = new MockLogger<ProductCreatedHandler>();

        [TestMethod]
        public async Task ShouldHandle()
        {
            var mockNewProduct = new Product
            {
                Id = Guid.Empty
            };
           
            ProductCreatedHandler sut = new ProductCreatedHandler(_logger.Object);
            await sut.Handle(new ProductCreated(mockNewProduct), CancellationToken.None);

            _logger.Verify(x =>
               x.Log(
                   LogLevel.Information,
                   0,
                   It.Is<object>(msg => msg.ToString() == $"Product {mockNewProduct.Id} was added to data base"),
                   null,
                   It.IsAny<Func<object, Exception, string>>()
               )
           );
        }
    }
}