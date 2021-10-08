using Application.Events;
using Application.Repositories;
using System.Linq;
using Domain.Entities;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation.Results;
using Application.Exceptions;

namespace Application.Commands.AddNewProduct
{
    public class AddNewProductCommandHandler : IRequestHandler<AddNewProductCommand, Product>
    {

        readonly IProductRepository _repository;
        readonly IMediator _mediator;

        public AddNewProductCommandHandler(IProductRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Product> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

            var result = await Save(request, cancellationToken);

            await _mediator.Publish(new ProductCreated(result));

            return result;
        }

        async Task<Product> Save(AddNewProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                UnitPrice = 0,
                CurrentStock = 0
            };
            _repository.Add(product);
            await _repository.SaveChangesAsync(cancellationToken);
            return product;
        }

        void Validate(AddNewProductCommand request)
        {
            var validator = new AddNewProductCommandValidator();
            ValidationResult results = validator.Validate(request);
            bool validationSucceed = results.IsValid;
            if (!validationSucceed)
            {
                var failures = results.Errors.ToList();
                StringBuilder message = new StringBuilder();
                failures.ForEach(f => { message.Append(f.ErrorMessage + Environment.NewLine); });
                throw new CQRSException(400, message.ToString());
            }
        }
    }
}