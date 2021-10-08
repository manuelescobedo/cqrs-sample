using Domain.Entities;
using System;
using MediatR;

namespace Application.Commands.AddNewProduct
{
    public class AddNewProductCommand : IRequest<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}