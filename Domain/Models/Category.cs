namespace Products_CQRS.Domain.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }


        //Egy kategóriához tartozó termékek listája
        public ICollection<Product> Products { get; set; }
    }
}
