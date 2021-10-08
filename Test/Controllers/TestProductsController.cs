using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using System.Linq;
using API.Models;
using Moq;
using MediatR;
using AutoMapper;
using Application.Commands.AddNewProduct;
using Application.Commands.UpdateProductCurrentStock;
using Application.Commands.UpdateProductUnitPrice;
using Application.Commands.DeleteProduct;
using System;
using System.Threading.Tasks;
using Domain.Entities;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.GetProductsByName;
using Application.Queries.FindOutStockProducts;
using System.Collections.Generic;
using Test.Doubles;

namespace Test.Controllers
{
    [TestClass]
    public class TestProductsController
    {
        readonly MockMediator _mediator = new MockMediator();
        readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        [TestMethod]
        public async Task ShouldAddProduct()
        {
            var product = new Product { Name = "Name" };
            _mediator.MockSend<AddNewProductCommand, Product>(product);

            var sut = new ProductsController(_mediator.Object, _mapper.Object);
            var actual = await sut.AddProduct(new AddNewProductCommand { Id = Guid.Empty, Name = "Name", Description = "Desc" }) as CreatedAtRouteResult;

            Assert.IsNotNull(actual);
            Assert.AreEqual(nameof(sut.GetProductsByName), actual.RouteName);
            Assert.AreEqual(product.Name, actual.RouteValues["Name"]);
        }

        [TestMethod]
        public async Task ShouldGetProductsByName()
        {
            GetProductsByNameQuery query = new GetProductsByNameQuery { Name = "Name" };
            List<Product> listProducts = new List<Product> {
                new Product {

                }
            };
            _mediator.MockSend< GetProductsByNameQuery, List<Product>>(listProducts);
            _mapper.Setup(m => m.Map<List<ProductDisplay>>(It.IsAny<List<Product>>())).Returns(new List<ProductDisplay> {
                new ProductDisplay {

                }
            });
            

            var sut = new ProductsController(_mediator.Object, _mapper.Object);
            IActionResult actual = await sut.GetProductsByName(query.Name);


            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            _mediator.Verify(m => m.Send(It.Is<GetProductsByNameQuery>(q => q.Name == query.Name), It.IsAny<CancellationToken>()));
            _mapper.Verify(m => m.Map<List<ProductDisplay>>(It.Is<List<Product>>(l => l.Any(i => i.Id == listProducts[0].Id))), Times.Once());
        }

        [TestMethod]
        public async Task ShouldGetProductsOutOfStock()
        {
            List<Product> listProducts = new List<Product> {
                new Product {

                }
            };
            _mediator.MockSend< FindOutOfStockProductsQuery, List<Product>>(listProducts);
            _mapper.Setup(m => m.Map<List<ProductInventory>>(It.IsAny<List<Product>>())).Returns(new List<ProductInventory> {
                new ProductInventory {

                }
            });

            var sut = new ProductsController(_mediator.Object, _mapper.Object);
            IActionResult actual = await sut.GetProductsOutOfStock();

            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            _mediator.Verify(m => m.Send(It.IsAny<FindOutOfStockProductsQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            _mapper.Verify(m => m.Map<List<ProductInventory>>(It.Is<List<Product>>(l => l.Any(i => i.Id == listProducts[0].Id))), Times.Once());
        }

        [TestMethod]
        public async Task ShouldUpdateProductInStock()
        {
            UpdateProductCurrentStockCommand command = new UpdateProductCurrentStockCommand
            {
                CurrentStock = 1,
                Id = Guid.NewGuid()
            };
            _mediator.MockSend<UpdateProductCurrentStockCommand>();

            var sut = new ProductsController(_mediator.Object, _mapper.Object);
            IActionResult actual = await sut.UpdateProductInStock(command);

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
            _mediator.Verify(m => m.Send(It.Is<UpdateProductCurrentStockCommand>(c => c.Id == command.Id && c.CurrentStock == command.CurrentStock), It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task ShouldUpdateProductUnitPrice()
        {
            UpdateProductUnitPriceCommand command = new UpdateProductUnitPriceCommand
            {
                Id = Guid.Empty,
                UnitPrice = 1

            };
            _mediator.MockSend< UpdateProductUnitPriceCommand>();

            var sut = new ProductsController(_mediator.Object, _mapper.Object);
            IActionResult actual = await sut.UpdateProductUnitPrice(command);

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
            _mediator.Verify(m => m.Send(It.Is<UpdateProductUnitPriceCommand>(c => c.Id == command.Id && c.UnitPrice == command.UnitPrice), It.IsAny<CancellationToken>()));
        }
        [TestMethod]
        public async Task ShouldDeleteProduct()
        {
            DeleteProductCommand command = new DeleteProductCommand
            {
                Id = Guid.Empty
            };
            _mediator.MockSend< DeleteProductCommand>();

            var sut = new ProductsController(_mediator.Object, _mapper.Object);
            IActionResult actual = await sut.DeleteProduct(command);

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
            _mediator.Verify(m => m.Send(It.Is<DeleteProductCommand>(c => c.Id == command.Id), It.IsAny<CancellationToken>()));
        }
    }
}
