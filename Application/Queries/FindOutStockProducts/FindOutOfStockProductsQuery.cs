using MediatR;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.Queries.FindOutStockProducts
{
    public class FindOutOfStockProductsQuery : IRequest<List<Product>>
    {
        
    }
}