using System.ComponentModel.DataAnnotations;
namespace Products_CQRS.Models

{
    public class DeleteCategoryRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
