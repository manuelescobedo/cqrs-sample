using MediatR;
using Domain.Entities;

namespace Application.Events
{
    public class ProductCurrentStockUpdated : INotification
    {
        public Product NewProduct { get; }

        public int OldStock { get; }

        public ProductCurrentStockUpdated(Product newProduct, int oldStock)
        {
            OldStock = oldStock;
            NewProduct = newProduct;
        }
    }
}