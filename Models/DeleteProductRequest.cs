using System.ComponentModel.DataAnnotations;
namespace Products_CQRS.Features.Products
{
    public class DeleteProductRequest
    {
        public int Id { get; set; }
    }
}
