using MediatR;
using Application.Events;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Application.Commands.DeleteProduct
{
    public class ProductDeletedHandler : INotificationHandler<ProductDeleted>
    {
        private readonly ILogger<ProductDeletedHandler> _logger;

        public ProductDeletedHandler(ILogger<ProductDeletedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductDeleted notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Product {notification.DeletedProduct.Id} was deleted.");
            return Task.CompletedTask;
        }
    }
}