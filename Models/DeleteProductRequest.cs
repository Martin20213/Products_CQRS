using System.ComponentModel.DataAnnotations;
namespace Products_CQRS.Models
{
    public class DeleteProductRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
