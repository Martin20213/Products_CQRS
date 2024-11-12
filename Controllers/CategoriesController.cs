using MediatR;
using Microsoft.AspNetCore.Mvc;

using Products_CQRS.Domain.Models;
using Products_CQRS.Features.Categories;
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
        public async Task<ActionResult<int>> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            if (command == null)
            {
                return BadRequest("Category data is invalid.");
            }

            var categoryId = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateCategory), new { id = categoryId }, categoryId);
        }
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            // Lekérdezzük a kategóriákat a MediatR segítségével
            var categories = await _mediator.Send(new GetCategoriesQuery());
            return Ok(categories);
        }


        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));

            if (result)
            {
                return NoContent(); // Törlés sikeres, nincs tartalom vissza
            }
            else
            {
                return NotFound("Category not found."); // Ha nem találjuk a kategóriát
            }
        }

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id, [FromBody] EditCategoryCommand command)
        {
            if (id != command.Id)
            {
                // Ha az ID nem egyezik, válaszoljunk 400-as hibával
                return BadRequest("ID mismatch");
            }

            // Meghívjuk az EditCategoryCommand-ot a MediatR segítségével
            var result = await _mediator.Send(command);

            if (result)
            {
                // Ha sikerült a frissítés, 200 OK választ küldünk
                return Ok("Category updated successfully");
            }

            // Ha nem található a kategória, válaszoljunk 404-es hibával
            return NotFound("Category not found");
        }
    }
}
