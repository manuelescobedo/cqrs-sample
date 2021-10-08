using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Application.Repositories;
using System;
using System.Linq;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Test.Doubles;

namespace Test.Repositories
{
    [TestClass]
    public class TestFindOutOfStockProducts
    {
        [TestMethod]
        public void ShouldFindOutOfStockProducts()
        {
            var list = new List<Product> {
                new Product {
                    Name = "Name",
                    UnitPrice = 100,
                    Id = Guid.Empty
                }
            };
            MockDbContext<Product> context = new MockDbContext<Product>(list);

            var sut = new ProductRepository(context.Object);

            var actual = sut.FindOutOfStockProducts();
            Assert.AreEqual(list.Count, actual.Count);

        }

        [TestMethod]
        public void ShouldNotFindOutOfStockProducts()
        {
            
            MockDbContext<Product> context = new MockDbContext<Product>();

            var sut = new ProductRepository(context.Object);

            var actual = sut.FindOutOfStockProducts();
            Assert.AreEqual(0, actual.Count);

        }
    }
}