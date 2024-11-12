using System.ComponentModel.DataAnnotations;
namespace Products_CQRS.Models
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public int Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
