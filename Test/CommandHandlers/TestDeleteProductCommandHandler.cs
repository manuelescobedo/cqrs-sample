
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Commands.DeleteProduct;
using Application.Exceptions;
using Moq;
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
    public class TestDeleteProductCommandHandler
    {
        MockProductRepository _repository = new MockProductRepository();
        MockMediator _mediator = new MockMediator();


        [TestMethod]

        public async Task ShouldHandle()
        {
            DeleteProductCommand mockCommand = new DeleteProductCommand
            {
                Id = Guid.Empty
            };
            _repository.MockGetById();

            DeleteProductCommandHandler sut = new DeleteProductCommandHandler(_repository.Object, _mediator.Object);
            var actual = await sut.Handle(mockCommand, CancellationToken.None);

            Assert.IsInstanceOfType(actual, typeof(Unit));
        }

        [TestMethod]

        public async Task ShouldPublishProductDeletedEvent()
        {
            DeleteProductCommand mockCommand = new DeleteProductCommand
            {
                Id = Guid.Empty
            };
            _repository.MockGetById();

            DeleteProductCommandHandler sut = new DeleteProductCommandHandler(_repository.Object, _mediator.Object);
            var actual = await sut.Handle(mockCommand, CancellationToken.None);

            _mediator.Verify(m => m.Publish(It.Is<ProductDeleted>(d => d.DeletedProduct.Id == mockCommand.Id), It.IsAny<CancellationToken>()));
        }

        [TestMethod]

        public async Task ShouldThrowExceptionWhenProductIsNotFound()
        {
            DeleteProductCommandHandler sut = new DeleteProductCommandHandler(_repository.Object, _mediator.Object);
            Func<Task> fn = () => sut.Handle(new DeleteProductCommand
            {
                Id = Guid.Empty
            }, CancellationToken.None);

            await Assert.ThrowsExceptionAsync<CQRSException>(fn);
        }
    }
}
