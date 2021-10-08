using Application.Repositories;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Test.Doubles
{
    public class MockProductRepository : Mock<IProductRepository>
    {
        public MockProductRepository()
        {
            Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            Setup(r => r.Update(It.IsAny<Guid>(), It.IsAny<Product>()));
            Setup(r => r.Delete(It.IsAny<Guid>()));
            Setup(r => r.Add(It.IsAny<Product>()));
        }

        public void MockGetById(Product dummyProduct)
        {
            Setup(r => r.Get(It.IsAny<Guid>())).Returns(dummyProduct);
        }

        public void MockGetById()
        {
            MockGetById(new Product());
        }

        public void MockFindOutOfStockProducts(List<Product> dummyList)
        {
            Setup(r => r.FindOutOfStockProducts()).Returns(dummyList);
        }

        public void MockGetProductsByName(List<Product> dummyList)
        {
            Setup(r => r.GetProductsByName(It.IsAny<string>())).Returns(dummyList);
        }

    }
}
