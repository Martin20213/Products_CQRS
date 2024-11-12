using MediatR;
using Microsoft.EntityFrameworkCore;
using Products_CQRS.Data;

namespace Products_CQRS.Features.Products
{
    public record EditProductCommand(int Id, string Name, int Price, int Rating) : IRequest<bool>;

    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public EditProductCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            // Keresd meg a terméket az ID alapján
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                // Ha nem találjuk a terméket, akkor nem sikerült a módosítás
                return false;
            }

            // Frissítsük a termék adatait
            product.Name = request.Name;
            product.Price = request.Price;
            product.Rating = request.Rating;

            // Frissített terméket mentjük el
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            // Visszatérünk true-val, jelezve, hogy a módosítás sikeres volt
            return true;
        }
    }

}
