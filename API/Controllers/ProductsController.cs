using Domain.Entities;
using System.Collections.Generic;
using Application.Commands.AddNewProduct;
using Application.Commands.UpdateProductCurrentStock;
using Application.Commands.UpdateProductUnitPrice;
using API.Models;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.GetProductsByName;
using AutoMapper;
using Application.Queries.FindOutStockProducts;
using Application.Commands.DeleteProduct;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMapper _mapper;


        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(AddNewProductCommand command)
        {
            Product result = await _mediator.Send(command);

            return CreatedAtRoute(nameof(GetProductsByName), new GetProductsByNameQuery { Name = result.Name }, result);
        }

        [HttpGet("{name}", Name = "GetProductsByName")]

        public async Task<IActionResult> GetProductsByName(string name)
        {
            List<Product> response = await _mediator.Send(new GetProductsByNameQuery { Name = name });

            List<ProductDisplay> result = _mapper.Map<List<ProductDisplay>>(response);

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsOutOfStock()
        {
            List<Product> response = await _mediator.Send(new FindOutOfStockProductsQuery());

            List<ProductInventory> result = _mapper.Map<List<ProductInventory>>(response);

            return Ok(result);
        }


        [HttpPut("stock")]
        public async Task<IActionResult> UpdateProductInStock(UpdateProductCurrentStockCommand request)
        {
            await _mediator.Send(request);

            return NoContent();
        }

        [HttpPut("unitPrice")]
        public async Task<IActionResult> UpdateProductUnitPrice(UpdateProductUnitPriceCommand request)
        {
            await _mediator.Send(request);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand request)
        {

            await _mediator.Send(request);

            return NoContent();
        }
    }
}