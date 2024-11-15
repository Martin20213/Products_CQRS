using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products_CQRS.Features.Categories;
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


        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductRequest request)
        {
            // Mappeljük a Requestet a Command-ra
            var command = request.Adapt<EditProductCommand>();

            // Ellenőrizzük, hogy az id a request-ben és a paraméterben egyezzen
            if (id != command.Id)
            {
                // Ha nem egyezik, 400-as hibát küldünk
                return BadRequest("ID mismatch");
            }

            // Meghívjuk a MediatR-t, hogy hajtsa végre a parancsot
            var result = await _mediator.Send(command);

            if (result)
            {
                // Ha a frissítés sikerült, 200 OK választ küldünk
                return Ok("Product updated successfully");
            }

            // Ha nem található a kategória, 404 Not Found választ küldünk
            return NotFound("Product not found");
        }

    }



}
