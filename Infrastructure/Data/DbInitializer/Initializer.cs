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

namespace Infrastructure.Data.DbInitializer
{
    public static class Initializer 
    {
        
        
        
        private static readonly (string CategoryName, string Name, string Brand, Dictionary<string, string[]> Attributes)[] productsConfigurations =
        {
            (
                CategoryName: "Мобильные телефоны",
                Name: "Samsung Galaxy S25",
                Brand: "Samsung",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Icy Blue", "Mint", "Navy", "Silver Shadow", "Pink Gold", "Coral Red", "Blue Black" } },
                    { "Память", new[] { "128GB", "256GB", "512GB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "Samsung Galaxy S25+",
                Brand: "Samsung",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Icy Blue", "Mint", "Navy", "Silver Shadow", "Pink Gold", "Coral Red", "Blue Black" } },
                    { "Память", new[] { "256GB", "512GB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "Samsung Galaxy S25 Ultra",
                Brand: "Samsung",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[]
                        {
                            "Titanium Silver Blue", "Titanium Black", "Titanium White Silver",
                            "Titanium Grey", "Titanium Jade Green", "Titanium Jet Black", "Titanium Pink Gold"
                        }
                    },
                    { "Память", new[] { "256GB", "512GB", "1TB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Розовый", "Голубой", "Белый", "Бирюзовый", "Чёрный" } },
                    { "Память", new[] { "128GB", "256GB", "512GB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16 Plus",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Розовый", "Голубой", "Белый", "Бирюзовый", "Чёрный" } },
                    { "Память", new[] { "128GB", "256GB", "512GB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16 Pro",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Титановый белый", "Титановый песочный", "Титановый натуральный", "Титановый чёрный" } },
                    { "Память", new[] { "128GB", "256GB", "512GB", "1TB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16 Pro Max",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Титановый белый", "Титановый песочный", "Титановый натуральный", "Титановый чёрный" } },
                    { "Память", new[] { "128GB", "256GB", "512GB", "1TB" } }
                }
            ),
            (
                CategoryName: "Мобильные телефоны",
                Name: "iPhone 16e",
                Brand: "Apple",
                Attributes: new Dictionary<string, string[]>
                {
                    { "Цвет", new[] { "Розовый", "Голубой", "Белый", "Бирюзовый", "Чёрный" } },
                    { "Память", new[] { "128GB", "256GB" } }
                }
            )
        };


        public static void Set(ModelBuilder modelBuilder, IMapper mapper)
        {

            // Пользователи
            Guid createdByUserId = modelBuilder.CreateDefaultUsers();

            //Поставщики
            Supplier supplier = modelBuilder.CreateDefaultSuppliers(createdByUserId);

            //Категории
            List<Category> categories = modelBuilder.CreateDefaultCategories(mapper, createdByUserId);
            
            //Атрибуты категорий
            (List<CategoryAttribute> categoryAttributes, Dictionary<Guid, List<CategoryAttributeValue>> categoryAttributeValues) 
                = modelBuilder.CreateDefaultCategoryAttributes(
                categories,
                productsConfigurations,
                createdByUserId
                );
            
            //Продавцы
            Seller seller = modelBuilder.CreateSellers(createdByUserId);
            
            //Бренды
            List<Brand> brands = modelBuilder.CreateDefaultBrands(createdByUserId);
            
            //Создаём список продуктов
            List<(Product product, List<ProductVariant> productVariants)> products = modelBuilder.CreateDefaultProducts(
                productsConfigurations, 
                categoryAttributes,
                categoryAttributeValues,
                categories[0].Id, 
                supplier.Id, 
                brands,
                createdByUserId);
             
            
               
          
            
            /*List<SellerOffer> sellerOffers = modelBuilder.CreateDefaultSellerOffers(
                productsConfigurations, 
                categoryAttributes,
                categoryAttributeValues,
                products, 
                seller, 
                createdByUserId);*/

            var brandApple = brands.Where(b => b.Name == "Apple").First();
            var brandSamsung = brands.Where(b => b.Name == "Samsung").First();

           

            //Добавляем SellerOffers (предложения продавца)
            /*modelBuilder.CreateSellerOffers(seller);*/

            var smartphoneCategory = categories.Where(c => c.Name == "Мобильные телефоны").First();

 

           

            var colorAttributeId = Guid.NewGuid();
            var storageAttributeId = Guid.NewGuid();

            modelBuilder.Entity<CategoryAttribute>().HasData(
                new CategoryAttribute
                {
                    Id = colorAttributeId, CategoryId = smartphoneCategory.Id, Name = "Цвет",
                    CreatedByUserId = createdByUserId
                },
                new CategoryAttribute
                {
                    Id = storageAttributeId, CategoryId = smartphoneCategory.Id, Name = "Встроенная память",
                    CreatedByUserId = createdByUserId
                }
            );

        
 
 

            modelBuilder.Entity<CategoryAttributeValue>().HasData(categoryAttributeValues);

        


         
        }
        
        /// <summary>
        /// Пример функции формирования SKU на базе названия, цвета, объёма памяти.
        /// Убираем пробелы/неугодные символы и соединяем.
        /// </summary>
        private static string GenerateSku(string productName, string color, string storage)
        {
            // Например "Samsung Galaxy S25" -> "SamsungGalaxyS25"
            var namePart = productName.Replace(" ", "");
            var colorPart = color.Replace(" ", "");
            return $"{namePart}-{colorPart}-{storage}";
        }

 


    }
}
