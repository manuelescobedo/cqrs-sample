using Domain.Entities;
using MediatR;
using Application.Repositories;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.Queries.GetProductsByName
{
    public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, List<Product>>
    {
        readonly IProductRepository _repository;

        public GetProductsByNameQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Product>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var list =  _repository.GetProductsByName(request.Name);

            return Task.FromResult(list);


        }
    }
}