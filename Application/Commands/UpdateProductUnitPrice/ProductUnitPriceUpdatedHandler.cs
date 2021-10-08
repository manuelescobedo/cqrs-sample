using Microsoft.Extensions.Logging;
using MediatR;
using Application.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.UpdateProductUnitPrice
{
    public class ProductUnitPriceUpdatedHandler : INotificationHandler<ProductUnitPriceUpdated>
    {
        private readonly ILogger<ProductUnitPriceUpdatedHandler> _logger;

        public ProductUnitPriceUpdatedHandler(ILogger<ProductUnitPriceUpdatedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductUnitPriceUpdated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"The UnitPrice of Product {notification.NewProduct.Id} was updated, new Value = {notification.NewProduct.UnitPrice} and last Value={notification.OldUnitPrice}.");
            return Task.CompletedTask;
        }
    }
}