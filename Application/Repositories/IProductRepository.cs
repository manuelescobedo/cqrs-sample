using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProductsByName(string name);

        List<Product> FindOutOfStockProducts();
    }
}