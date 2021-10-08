using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Repositories;
using System;
using Domain.Entities;
using Test.Doubles;

namespace Test.Repositories
{
    [TestClass]
    public class TestGetProductsByName
    {
        [TestMethod]
        public void ShouldGetAllProductsByNameWhenNameInputIsEmpty()
        {
            var list = new List<Product> { new Product { Name = "Name", UnitPrice = 100, Id = Guid.Empty } };
            MockDbContext<Product> context = new MockDbContext<Product>(list);

            var sut = new ProductRepository(context.Object);

            var actual = sut.GetProductsByName("");
            Assert.AreEqual(list.Count, actual.Count);

        }

        [TestMethod]
        public void ShouldGetProductByName()
        {
            var list = new List<Product> { new Product { Name = "Name", UnitPrice = 100, Id = Guid.Empty } };
            MockDbContext<Product> context = new MockDbContext<Product>(list);

            var sut = new ProductRepository(context.Object);

            var actual = sut.GetProductsByName("Name");
            Assert.AreEqual(1, actual.Count);

        }

        [TestMethod]
        public void ShouldDontGetProductsByName()
        {
            
            var list = new List<Product> {
                new Product { Name = "Name", UnitPrice = 100, Id = Guid.Empty },
                new Product { Name = "Eman", UnitPrice = 100, Id = Guid.Empty }
            };
            MockDbContext<Product> context = new MockDbContext<Product>(list);

            var sut = new ProductRepository(context.Object);

            var actual = sut.GetProductsByName("Nombre");
            Assert.AreEqual(0, actual.Count);

        }
    }
}