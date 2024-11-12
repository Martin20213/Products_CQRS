using MediatR;
using Microsoft.EntityFrameworkCore;
using Products_CQRS.Data;
using Products_CQRS.Domain.Models;


namespace Products_CQRS.Features.Products

{
    public record DeleteProductsCommand(int Id) : IRequest<bool>;

    public class DeleteProductsCommandHandler : IRequestHandler<DeleteProductsCommand, bool>

    {
        private readonly ApplicationDbContext _context;

    public DeleteProductsCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProductsCommand request, CancellationToken cancellationToken)
    {
        // Keresd meg a terméket az ID alapján
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            // Ha nem találjuk a terméket, akkor nem sikerült a törlés
            return false;
        }

        // Töröljük a terméket
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        // Visszatérünk true-val, jelezve, hogy a törlés sikeres volt
        return true;
    }
}

}
