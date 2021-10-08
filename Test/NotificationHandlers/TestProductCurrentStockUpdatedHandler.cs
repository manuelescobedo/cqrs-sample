using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Application.Commands.UpdateProductCurrentStock;
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
    public class TestProductCurrentStockUpdatedHandler
    {
        MockLogger<ProductCurrentStockUpdatedHandler> _logger = new MockLogger<ProductCurrentStockUpdatedHandler>();

        [TestMethod]
        public async Task ShouldHandle()
        {
            Product mockNewProduct = new Product(); int mockOldStock = 0;
            var mockProductCurrentStockUpdated = new ProductCurrentStockUpdated(
                mockNewProduct, mockOldStock
            );

            ProductCurrentStockUpdatedHandler sut = new ProductCurrentStockUpdatedHandler(_logger.Object);
            await sut.Handle(mockProductCurrentStockUpdated, CancellationToken.None);

            _logger.Verify(l =>
               l.Log(
                   LogLevel.Information,
                   0,
                   It.Is<object>(msg => msg.ToString() == $"The CurrentStock of Product {mockProductCurrentStockUpdated.NewProduct.Id} was updated, new Value = {mockProductCurrentStockUpdated.NewProduct.CurrentStock} and last Value={mockProductCurrentStockUpdated.OldStock}."),
                   null,
                   It.IsAny<Func<object, Exception, string>>()
               )
           );
        }
    }
}