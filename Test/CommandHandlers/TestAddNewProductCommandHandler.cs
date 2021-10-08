using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Exceptions;
using Moq;
using Application.Commands.AddNewProduct;
using System;
using System.Threading.Tasks;
using System.Threading;
using Application.Events;
using Test.Doubles;

namespace Test.CommandHandlers
{
    [TestClass]
    public class TestAddNewProductCommandHandler
    {
        MockProductRepository _repository = new MockProductRepository();
        MockMediator _mediator = new MockMediator();

        [TestMethod]
        public async Task ShouldHandle()
        {
            AddNewProductCommandHandler sut = new AddNewProductCommandHandler(
                _repository.Object,
                _mediator.Object
            );

            var mock = new AddNewProductCommand
            {
                Name = "Name",
                Description = "Description",
                Id = Guid.Empty
            };
            var actual = await sut.Handle(mock, CancellationToken.None);

            Assert.AreEqual(actual.Id, mock.Id);
            Assert.AreEqual(actual.Name, mock.Name);
            Assert.AreEqual(actual.Description, mock.Description);
            Assert.AreEqual(actual.UnitPrice, 0);
            Assert.AreEqual(actual.CurrentStock, 0);
        }

        [TestMethod]
        public async Task ShouldPublishProductCreatedEvent()
        {
            AddNewProductCommandHandler sut = new AddNewProductCommandHandler(
                            _repository.Object,
                            _mediator.Object
                        );
            var mock = new AddNewProductCommand
            {
                Name = "Name",
                Description = "Description",
                Id = Guid.Empty
            };

            await sut.Handle(mock, CancellationToken.None);

            _mediator.Verify(m => m.Publish(It.Is<ProductCreated>(p =>
                p.NewProduct.Id == mock.Id &&
                p.NewProduct.Name == mock.Name &&
                p.NewProduct.Description == mock.Description &&
                p.NewProduct.UnitPrice == 0 &&
                p.NewProduct.CurrentStock == 0
            ), CancellationToken.None));
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenProductIsInvalid()
        {
            AddNewProductCommandHandler sut = new AddNewProductCommandHandler(
                _repository.Object,
                _mediator.Object
            );

            var mock = new AddNewProductCommand
            {
                Name = "",
                Description = "",
                Id = Guid.Empty
            };
            Func<Task> fn = () => sut.Handle(mock, CancellationToken.None);

            await Assert.ThrowsExceptionAsync<CQRSException>(fn);


        }
    }
}