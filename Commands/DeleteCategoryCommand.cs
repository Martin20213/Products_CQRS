using MediatR;
using Products_CQRS.Data;
using Microsoft.EntityFrameworkCore;
namespace Products_CQRS.Commands
{
    public record DeleteCategoryCommand(int Id) : IRequest<bool>;


    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // Keresd meg a kategóriát az ID alapján
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category == null)
            {
                // Ha nem találjuk a kategóriát, akkor nem sikerült a törlés
                return false;
            }

            // Töröljük a kategóriát
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
