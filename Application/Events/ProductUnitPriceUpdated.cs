using Domain.Entities;
using MediatR;

namespace Application.Events
{
    public class ProductUnitPriceUpdated : INotification
    {
        public Product NewProduct { get; }

        public decimal OldUnitPrice { get; }


        public ProductUnitPriceUpdated(Product newProduct, decimal oldUnitPrice)
        {
            NewProduct = newProduct;
            OldUnitPrice = oldUnitPrice;
        }
    }
}