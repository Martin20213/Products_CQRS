using MediatR;
using Microsoft.EntityFrameworkCore;
using Products_CQRS.Data;
using Products_CQRS.Domain.Models;
namespace Products_CQRS.Features.Categories

{
    public record GetCategoriesQuery : IRequest<List<Category>>;

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
    {
        private readonly ApplicationDbContext _context;

        public GetCategoriesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            // Az összes kategóriát lekérdezi az adatbázisból
            return await _context.Categories.ToListAsync(cancellationToken);
        }
    }

}
