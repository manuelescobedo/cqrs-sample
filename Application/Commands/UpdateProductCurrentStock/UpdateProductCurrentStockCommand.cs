using Domain.Entities;
using MediatR;
using System;

namespace Application.Commands.UpdateProductCurrentStock
{
    public class UpdateProductCurrentStockCommand : IRequest
    {
        public Guid Id { get; set; }
        public int CurrentStock { get; set; }
    }
}