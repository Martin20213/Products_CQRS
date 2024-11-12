using Mapster;
using Products_CQRS.Features.Products;
using Products_CQRS.Models;

namespace Products_CQRS.Mappings
{
    public class MappingConfig
    {
        public static void RegisterMappings() 
        {
            //itt definialjuk a createproductrequest -> createproduct command mapelést
            TypeAdapterConfig<CreateProductRequest, CreateProductCommand>.NewConfig();
        }
    }
}
