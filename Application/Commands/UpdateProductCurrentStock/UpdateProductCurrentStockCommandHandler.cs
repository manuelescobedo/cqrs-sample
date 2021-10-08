using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using Application.Events;
using Application.Exceptions;
using Domain.Entities;
using System;

namespace Application.Commands.UpdateProductCurrentStock
{
    public class UpdateProductCurrentStockCommandHandler : IRequestHandler<UpdateProductCurrentStockCommand>
    {
        readonly IMediator _mediator;
        readonly IProductRepository _repository;

        public UpdateProductCurrentStockCommandHandler(IMediator mediator, IProductRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductCurrentStockCommand request, CancellationToken token)
        {
            var product = _repository.Get(request.Id);
            if (product == null)
                throw new CQRSException(404, $"{request.Id} not found");

            var oldStock = product.CurrentStock;

            await Save(product, request, token);

            await _mediator.Publish(new ProductCurrentStockUpdated(product, oldStock));

            return Unit.Value;
        }

        async Task Save(Product product, UpdateProductCurrentStockCommand request, CancellationToken token)
        {
            product.CurrentStock = request.CurrentStock;
            _repository.Update<Guid>(request.Id, product);
            await _repository.SaveChangesAsync(token);
        }
    }
}