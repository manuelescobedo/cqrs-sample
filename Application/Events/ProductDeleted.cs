using MediatR;
using Domain.Entities;

namespace Application.Events
{
    public class ProductDeleted : INotification
    {
        public Product DeletedProduct { get; }

        public ProductDeleted(Product deletedProduct)
        {
            DeletedProduct = deletedProduct;
        }
    }
}