using Mapster;
using Products_CQRS.Features.Categories;
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

            TypeAdapterConfig<CreateCategoryRequest, CreateCategoryCommand>.NewConfig();

            TypeAdapterConfig<DeleteProductRequest, DeleteProductsCommand>.NewConfig();

            TypeAdapterConfig<DeleteCategoryRequest, DeleteCategoryCommand>.NewConfig();

            TypeAdapterConfig<EditCategoryRequest, EditCategoryCommand>.NewConfig();
            

        }
    }
}
