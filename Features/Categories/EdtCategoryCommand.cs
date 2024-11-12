using MediatR;
using Microsoft.EntityFrameworkCore;
using Products_CQRS.Data;

namespace Products_CQRS.Features.Categories
{
    public record EditCategoryCommand(int Id, string Name) : IRequest<bool>;

    // A parancsot kezelő osztály
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public EditCategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            // Keresd meg a kategóriát az ID alapján
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category == null)
            {
                // Ha nem találjuk a kategóriát, akkor nem sikerült a módosítás
                return false;
            }

            // Frissítsük a kategória nevét
            category.Name = request.Name;

            // Mentsük el a módosított kategóriát
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
