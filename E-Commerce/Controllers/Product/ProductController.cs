using E_commerce.Dtos.Product;
using E_commerce.Features.Products.Command.Product;
using E_commerce.Features.Products.Quaries.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] RequestProductDtos createProductDtos)
        {
            var command = new CreateProductCommand
            {
                ProductName = createProductDtos.Name,
                Description = createProductDtos.Description,
                Price = createProductDtos.Price,
                Stock = createProductDtos.Stock
            };

            await _mediator.Send(command);
            return Ok(new { Message = "Product created successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] RequestProductDtos updateProductDtos)
        {
            var command = new UpdateProductCommand
            {
                Id = id,
                ProductName = updateProductDtos.Name,
                Description = updateProductDtos.Description,
                Price = updateProductDtos.Price,
                Stock = updateProductDtos.Stock
            };

            await _mediator.Send(command);
            return Ok(new { Message = "Product updated successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            await _mediator.Send(command);
            return Ok(new { Message = "Product deleted successfully." });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var query = new GetByIDProductQuaries { Id = id };
                var product = await _mediator.Send(query);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductQuaries();
            var products = await _mediator.Send(query);
            return Ok(products);
        }
    }
}
