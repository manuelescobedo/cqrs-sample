using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Domain.Entities;
using System.Threading;
using Application.Events;
using Microsoft.Extensions.Logging;
using Application.Commands.DeleteProduct;
using Test.Doubles;

namespace Test.NotificationHandlers
{
    [TestClass]

    public class TestProductDeletedHandler
    {
        MockLogger<ProductDeletedHandler> _logger = new MockLogger<ProductDeletedHandler>();

        [TestMethod]
        public async Task ShouldHandle()
        {
            var mockDeletedProduct = new ProductDeleted
            (new Product
            {
                Id = Guid.Empty
            });
            ProductDeletedHandler sut = new ProductDeletedHandler(_logger.Object);

            await sut.Handle(mockDeletedProduct, CancellationToken.None);

            _logger.Verify(l =>
               l.Log(
                   LogLevel.Information,
                   0,
                   It.Is<object>(msg => msg.ToString() == $"Product {mockDeletedProduct.DeletedProduct.Id} was deleted."),
                   null,
                   It.IsAny<Func<object, Exception, string>>()
               )
           );
        }
    }
}