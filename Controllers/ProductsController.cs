using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products_CQRS.Features.Products;
using Products_CQRS.Models;

namespace Products_CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var command = request.Adapt<CreateProductCommand>();
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProducts), new { id = productId }, productId);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Create the request object
            var request = new DeleteProductRequest { Id = id };

            // Map the request to a command using Mapster
            var command = request.Adapt<DeleteProductsCommand>();

            // Call MediatR to handle the command
            var result = await _mediator.Send(command);

            if (result)
            {
                // If deletion was successful, return 200 OK
                return Ok("Product deleted successfully");
            }

            // If product is not found, return 404 Not Found
            return NotFound("Product not found");
        }


        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductCommand command)
        {
            if (id != command.Id)
            {
                // Ha az ID nem egyezik, válaszoljunk 400-as hibával
                return BadRequest("ID mismatch");
            }

            // Meghívjuk az EditProductCommand-ot a MediatR segítségével
            var result = await _mediator.Send(command);

            if (result)
            {
                // Ha sikerült a módosítás, 200 OK választ küldünk
                return Ok("Product updated successfully");
            }

            // Ha nem található a termék, válaszoljunk 404-es hibával
            return NotFound("Product not found");
        }

    }



}
