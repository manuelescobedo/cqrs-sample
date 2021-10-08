using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.GetProductsByName
{
    public class GetProductsByNameQuery : IRequest<List<Product>>
    {
        public string Name { get; set; }
    }
}