using System;
using MediatR;

namespace Application.Commands.UpdateProductUnitPrice
{
    public class UpdateProductUnitPriceCommand : IRequest
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }

    }
}