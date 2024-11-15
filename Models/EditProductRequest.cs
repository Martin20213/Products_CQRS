using System.ComponentModel.DataAnnotations;

namespace Products_CQRS.Models
{
    public class EditProductRequest
    {

        public int Id { get; set; }

       
        public string Name { get; set; }

       
        public int Price { get; set; }

        public int Rating { get; set; }


    }
}
