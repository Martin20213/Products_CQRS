using System.ComponentModel.DataAnnotations;

namespace Products_CQRS.Models
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; }

    }
}
