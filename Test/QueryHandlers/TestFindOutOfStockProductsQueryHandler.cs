using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Entities;
using Application.Repositories;
using Application.Queries.FindOutStockProducts;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Test.Doubles;

namespace Test.QueryHandlers
{
    [TestClass]
    public class TestFindOutOfStockProductsQueryHandler
    {
        MockProductRepository _repository = new MockProductRepository();

        [TestMethod]
        public async Task ShouldHandle()
        {
            var mockListOfProducts = new List<Product>();
            _repository.MockFindOutOfStockProducts(mockListOfProducts);
            FindOutOfStockProductsQueryHandler sut = new FindOutOfStockProductsQueryHandler(_repository.Object);

            var actual = await sut.Handle(new FindOutOfStockProductsQuery
            {

            }, CancellationToken.None);

            Assert.AreEqual(mockListOfProducts.Count, actual.Count);
        }
    }
}