
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Exceptions;
using Moq;
using Application.Commands.UpdateProductCurrentStock;
using MediatR;
using System;
using System.Threading.Tasks;
using Domain.Entities;
using System.Threading;
using Application.Events;
using Application.Repositories;
using Test.Doubles;

namespace Test.CommandHandlers
{
    [TestClass]

    public class TestUpdateProductCurrentStockCommandHandler
    {
        MockProductRepository _repository = new MockProductRepository();
        MockMediator _mediator = new MockMediator();

        [TestMethod]
        public async Task ShouldHandle()
        {
            var mockProduct = new Product();
            var mockCommand = new UpdateProductCurrentStockCommand
            {
                CurrentStock = 1

            };
            _repository.MockGetById(mockProduct);

            var sut = new UpdateProductCurrentStockCommandHandler(_mediator.Object, _repository.Object);
            var actual = await sut.Handle(mockCommand, CancellationToken.None);


            _repository.Verify(r => r.Update<Guid>(mockProduct.Id, It.Is<Product>(p => p.CurrentStock == mockCommand.CurrentStock)));

            Assert.IsInstanceOfType(actual, typeof(Unit));

        }

        [TestMethod]
        public async Task ShouldPublishProductCurrentStockUpdatedEvent()
        {
            var mockProduct = new Product();
            var mockCommand = new UpdateProductCurrentStockCommand
            {
                CurrentStock = 1

            };
            _repository.MockGetById();

            var sut = new UpdateProductCurrentStockCommandHandler(_mediator.Object, _repository.Object);
            var actual = await sut.Handle(mockCommand, CancellationToken.None);



            _mediator.Verify(r => r.Publish(It.Is<ProductCurrentStockUpdated>(u => u.OldStock == mockProduct.CurrentStock && u.NewProduct.CurrentStock == mockCommand.CurrentStock), It.IsAny<CancellationToken>()));


        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenProductIsNotFound()
        {
            var mockCommand = new UpdateProductCurrentStockCommand
            {
                CurrentStock = 1
            };

            var sut = new UpdateProductCurrentStockCommandHandler(_mediator.Object, _repository.Object);
            Func<Task> fn = () => sut.Handle(mockCommand, CancellationToken.None);


            await Assert.ThrowsExceptionAsync<CQRSException>(fn);

        }
    }
}