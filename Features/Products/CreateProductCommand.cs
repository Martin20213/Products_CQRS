using MediatR;
using Microsoft.EntityFrameworkCore;
using Products_CQRS.Data;
using Products_CQRS.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Products_CQRS.Features.Products
{
    public record CreateProductCommand(string Name, int Price, int CategoryId) : IRequest<int>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateProductCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Kategória ellenőrzés CategoryId alapján az adatbázisban
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (category == null)
            {
                // Naplózzuk a hibát
                throw new ArgumentException($"Category with Id {request.CategoryId} not found.");
            }

            // Új termék létrehozása
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

    }
}
