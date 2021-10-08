
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Exceptions;
using Moq;
using Application.Commands.UpdateProductUnitPrice;
using MediatR;
using System;
using System.Threading.Tasks;
using Domain.Entities;
using System.Threading;
using Application.Events;
using Test.Doubles;

namespace Test.CommandHandlers
{
    [TestClass]
    public class TestUpdateProductUnitPriceCommandHandler
    {
        MockProductRepository _repository = new MockProductRepository();
        MockMediator _mediator = new MockMediator();

        [TestMethod]
        public async Task ShouldHandle()
        {
            var mockProduct = new Product();
            var mockCommand = new UpdateProductUnitPriceCommand
            {
                UnitPrice = 100
            };
            _repository.MockGetById(mockProduct);
            

            var sut = new UpdateProductUnitPriceCommandHandler(_repository.Object, _mediator.Object);
            var actual = await sut.Handle(mockCommand, CancellationToken.None);


            _repository.Verify(r => r.Update(mockProduct.Id, It.Is<Product>(p => p.UnitPrice == mockCommand.UnitPrice)));

            Assert.IsInstanceOfType(actual, typeof(Unit));

        }

        [TestMethod]
        public async Task ShouldPublishProductUnitPriceUpdatedEvent()
        {
            var mockProduct = new Product();
            var mockCommand = new UpdateProductUnitPriceCommand
            {
                UnitPrice = 100

            };
            _repository.MockGetById();
            

            var sut = new UpdateProductUnitPriceCommandHandler(_repository.Object, _mediator.Object);
            var actual = await sut.Handle(mockCommand, CancellationToken.None);



            _mediator.Verify(r => r.Publish(It.Is<ProductUnitPriceUpdated>(
                u => u.OldUnitPrice == mockProduct.UnitPrice && u.NewProduct.UnitPrice == mockCommand.UnitPrice
            ), It.IsAny<CancellationToken>()));


        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenProductIsNotFound()
        {
            var mockCommand = new UpdateProductUnitPriceCommand
            {
                UnitPrice = 100
            };

            var sut = new UpdateProductUnitPriceCommandHandler(_repository.Object, _mediator.Object);
            Func<Task> fn = () => sut.Handle(mockCommand, CancellationToken.None);


            await Assert.ThrowsExceptionAsync<CQRSException>(fn);

        }
    }
}