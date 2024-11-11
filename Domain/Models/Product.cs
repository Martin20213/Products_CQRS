using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Products_CQRS.Domain.Models
{
    public class Product
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public int Rating { get; set; }

        public int CategoryId { get; set; }

        //navigációs tulajdonság
        [JsonIgnore]
        public Category Category { get; set; }

    }
}
