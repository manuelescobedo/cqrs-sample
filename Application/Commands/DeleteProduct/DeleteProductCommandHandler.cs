using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Repositories;
using Application.Exceptions;
using Application.Events;

namespace Application.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler: IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IMediator _mediator;

        public DeleteProductCommandHandler(IProductRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _repository.Get(request.Id);
            if (product == null)
                throw new CQRSException(404, $"{request.Id} product not found");

            _repository.Delete(product.Id);
            
            await _repository.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new ProductDeleted(product));

            return Unit.Value;
        }

    }
}