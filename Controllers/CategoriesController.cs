using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using Products_CQRS.Domain.Models;
using Products_CQRS.Features.Categories;
using Products_CQRS.Features.Products;
using Products_CQRS.Models;


namespace Products_CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            // Átalakítjuk a DTO-t egy MediatR kérésre
            var command = request.Adapt<CreateCategoryCommand>();
            var categoryId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategories), new { id = categoryId }, categoryId);
        }
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            // Lekérdezzük a kategóriákat a MediatR segítségével
            var categories = await _mediator.Send(new GetCategoriesQuery());
            return Ok(categories);
        }


        // DELETE: api/products/{id}
        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            // Create the request object
            var request = new DeleteCategoryRequest { Id = id };

            // Map the request to a command using Mapster
            var command = request.Adapt<DeleteCategoryCommand>();

            // Call MediatR to handle the command
            var result = await _mediator.Send(command);

            if (result)
            {
                // If deletion was successful, return 200 OK
                return Ok("Category deleted successfully");
            }

            // If product is not found, return 404 Not Found
            return NotFound("Category not found");
        }

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id, [FromBody] EditCategoryRequest request)
        {
            // Mappeljük a Requestet a Command-ra
            var command = request.Adapt<EditCategoryCommand>();

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
                return Ok("Category updated successfully");
            }

            // Ha nem található a kategória, 404 Not Found választ küldünk
            return NotFound("Category not found");
        }

    }
}
