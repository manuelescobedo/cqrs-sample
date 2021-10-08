using Domain.Entities;
using System.Collections.Generic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;

namespace Application.Queries.FindOutStockProducts
{
    public class FindOutOfStockProductsQueryHandler : IRequestHandler<FindOutOfStockProductsQuery, List<Product>>
    {
        readonly IProductRepository _repository;

        public FindOutOfStockProductsQueryHandler(IProductRepository repository) {
            _repository = repository;
        }

        public Task<List<Product>> Handle(FindOutOfStockProductsQuery request, CancellationToken cancellationToken) {
            var products = _repository.FindOutOfStockProducts(); 

            return Task.FromResult(products);
        }
    }
}