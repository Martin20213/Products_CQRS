﻿
using MediatR;
using Products_CQRS.Data;
using Products_CQRS.Domain.Models;
namespace Products_CQRS.Commands

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
            // Kategória ellenőrzés
            var category = await _context.Categories.FindAsync(request.CategoryId);
            if (category == null)
            {
                // Ha nem találjuk a kategóriát, hibát dobunk.
                throw new ArgumentException("Category not found.");
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
