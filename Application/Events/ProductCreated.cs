using Domain.Entities;
using MediatR;

namespace Application.Events
{
    public class ProductCreated : INotification
    {
        public Product NewProduct { get; }

        public ProductCreated(Product newProduct)
        {
            NewProduct = newProduct;
        }
    }
}