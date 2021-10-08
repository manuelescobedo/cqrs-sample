using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Entities;
using Application.Repositories;
using Application.Queries.GetProductsByName;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Test.Doubles;

namespace Test.QueryHandlers
{
    [TestClass]
    public class TestGetProductsByNameQueryHandler
    {
        MockProductRepository _repository = new MockProductRepository();

        [TestMethod]
        public async Task ShouldHandle()
        {
            var mockListOfProducts = new List<Product>();
            var mockQuery = new GetProductsByNameQuery
            {
                Name = "name"
            };
            _repository.MockGetProductsByName(mockListOfProducts);
            GetProductsByNameQueryHandler sut = new GetProductsByNameQueryHandler(_repository.Object);

            var actual = await sut.Handle(mockQuery, CancellationToken.None);

            _repository.Verify(r => r.GetProductsByName(mockQuery.Name));
            Assert.AreEqual(mockListOfProducts.Count, actual.Count);

        }
    }
}