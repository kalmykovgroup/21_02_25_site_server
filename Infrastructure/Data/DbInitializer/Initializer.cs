using Domain.Entities.AddressesSpace;
using Domain.Entities.AddressesSpace.Heirs;
using Domain.Entities.BrandSpace;
using Domain.Entities.CategorySpace;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace;
using Domain.Entities.SupplierSpace; 
using Domain.Entities.UserSpace; 
using Microsoft.EntityFrameworkCore; 
using AutoMapper;
using Infrastructure.Data.DbInitializer.DataClasses;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.DbInitializer
{
    public static class Initializer 
    {
        
        public static void Set(ModelBuilder modelBuilder, IMapper mapper, ILogger logger)
        {
            var productsConfigurations = IPhone.ProductsConfigurations.Concat(Samsung.ProductsConfigurations).ToArray(); 
            // Пользователи
            Guid createdByUserId = modelBuilder.CreateDefaultUsers();

            //Поставщики
            Supplier supplier = modelBuilder.CreateDefaultSuppliers(createdByUserId);

            //Категории
            List<Category> categories = modelBuilder.CreateDefaultCategories(mapper, createdByUserId, logger);
            
            //Атрибуты категорий
            (List<CategoryAttribute> categoryAttributes, Dictionary<Guid, List<CategoryAttributeValue>> categoryAttributeValues) 
                = modelBuilder.CreateDefaultCategoryAttributes(
                categories,
                productsConfigurations,
                createdByUserId, logger
                );
            
            //Продавцы
            Seller seller = modelBuilder.CreateSellers(createdByUserId);
            
            //Бренды
            List<Brand> brands = modelBuilder.CreateDefaultBrands(createdByUserId);
            
            //Создаём список продуктов
            List<(Product product, List<ProductVariant> productVariants, List<ProductVariantAttributeValue> productVariantAttributeValues)> productVariants = modelBuilder.CreateDefaultProducts(
                productsConfigurations, 
                categoryAttributes,
                categoryAttributeValues,
                supplier.Id, 
                categories,
                brands,
                createdByUserId, logger);
            
            //Добавляем SellerOffers (предложения продавца)
            List<SellerOffer> sellerOffers = modelBuilder.CreateDefaultSellerOffers(
                productVariants, 
                categoryAttributes,
                categoryAttributeValues, 
                seller, 
                createdByUserId);
 
        }
        
   


    }
}
