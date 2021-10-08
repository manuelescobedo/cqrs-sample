
using Domain.Entities;
using System;
using MediatR;
using Application.Repositories;
using Application.Events;
using Application.Exceptions;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Commands.UpdateProductUnitPrice
{
    public class UpdateProductUnitPriceCommandHandler : IRequestHandler<UpdateProductUnitPriceCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IMediator _mediator;

        public UpdateProductUnitPriceCommandHandler(IProductRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(UpdateProductUnitPriceCommand request, CancellationToken cancellationToken)
        {
            var product = _repository.Get(request.Id);
            if (product == null)
                throw new CQRSException(404, $"{request.Id} product not found");

            var oldUnitPrice = product.UnitPrice;

            await Save(product, request, cancellationToken);

            //notification
            await _mediator.Publish(new ProductUnitPriceUpdated(product, oldUnitPrice));

            return Unit.Value;
        }

        async Task Save(Product product, UpdateProductUnitPriceCommand request, CancellationToken token)
        {
            product.UnitPrice = request.UnitPrice;
            _repository.Update(request.Id, product);
            await _repository.SaveChangesAsync(token);
        }
    }
}