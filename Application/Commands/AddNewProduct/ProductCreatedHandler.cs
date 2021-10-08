using Microsoft.Extensions.Logging;
using Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.AddNewProduct
{
    public class ProductCreatedHandler : INotificationHandler<ProductCreated>
    {
        readonly ILogger<ProductCreatedHandler> _logger;
        public ProductCreatedHandler(ILogger<ProductCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Product {notification.NewProduct.Id} was added to data base");
            return Task.CompletedTask;
        }
    }
}