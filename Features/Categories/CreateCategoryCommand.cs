using MediatR;
using Products_CQRS.Data;
using Products_CQRS.Domain.Models;
namespace Products_CQRS.Features.Categories
{
    public record CreateCategoryCommand(string Name) : IRequest<int>;


    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateCategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name  // Kategória neve a parancsból
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);
            return category.Id;  // A kategória ID-ja, amit visszaadunk
        }
    }

}
