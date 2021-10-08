using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Domain.Entities;
using System.Threading;
using Application.Events;
using Microsoft.Extensions.Logging;
using Application.Commands.UpdateProductUnitPrice;
using System;
using Test.Doubles;

namespace Test.NotificationHandlers
{
    [TestClass]

    public class TestProductUnitPriceUpdatedHandler
    {
        MockLogger<ProductUnitPriceUpdatedHandler> _logger = new MockLogger<ProductUnitPriceUpdatedHandler>();

        [TestMethod]
        public async Task ShouldHandle()
        {
            Product mockNewProduct = new Product(); decimal mockOldUnitPrice = 0;
            var mockProductCurrentStockUpdated = new ProductUnitPriceUpdated(
                mockNewProduct, mockOldUnitPrice
            );
            ProductUnitPriceUpdatedHandler sut = new ProductUnitPriceUpdatedHandler(_logger.Object);

            await sut.Handle(mockProductCurrentStockUpdated, CancellationToken.None);

            _logger.Verify(l =>
               l.Log(
                   LogLevel.Information,
                   0,
                   It.Is<object>(msg => msg.ToString() == $"The UnitPrice of Product {mockProductCurrentStockUpdated.NewProduct.Id} was updated, new Value = {mockProductCurrentStockUpdated.NewProduct.UnitPrice} and last Value={mockProductCurrentStockUpdated.OldUnitPrice}."),
                   null,
                   It.IsAny<Func<object, Exception, string>>()
               )
           );
        }
    }
}