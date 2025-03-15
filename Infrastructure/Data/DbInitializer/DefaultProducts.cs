using Domain.Entities.BrandSpace;
using Domain.Entities.CategorySpace;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace;
using Microsoft.EntityFrameworkCore; 

namespace Infrastructure.Data.DbInitializer;

public static class DefaultProducts
{
    public static  List<(Product product, List<ProductVariant> productVariants)> CreateDefaultProducts(
        this ModelBuilder modelBuilder, 
        (string CategoryName, string Name, string Brand, Dictionary<string, string[]> Attributes)[] productsConfigurations, 
        List<CategoryAttribute> categoryAttributes,
        Dictionary<Guid, List<CategoryAttributeValue>> categoryAttributeValues,
        Guid categoryId,
        Guid supplierId,
        List<Brand> brands,
        Guid createdByUserId)
    {
        
        List<(Product product, List<ProductVariant> productVariants)> productsWithVariants = new List<(Product product, List<ProductVariant> productVariants)>();
        
        // Создаем продукты, item -> { categoryName, name, brand, attributes }
        // "iPhone 16 Pro Max"
        foreach (var item in productsConfigurations)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                CategoryId = categoryId,
                BrandId = brands.Where(b => b.Name == item.Brand).First().Id,

                SupplierId = supplierId,
                NumberOfReviews = 0,
                IsActive = true,
                Description = $"Супер мобила {item.Brand}",
                CreatedByUserId = createdByUserId
            };
            
            var productVariants = new List<ProductVariant>();
            var productVariantAttributeValues = new List<ProductVariantAttributeValue>();
            // Проход по атрибутам продукта (color, storage и т.д.) ("Память", new[] { "128GB", "256GB", "512GB" })
            // Attributes: new Dictionary<string, string[]>
            // {
            //   { "Цвет", new[] { "Титановый белый", "Титановый песочный", "Титановый натуральный", "Титановый чёрный" } },
            //   { "Память", new[] { "128GB", "256GB", "512GB", "1TB" } } 
            // }

            // Вызываем метод, который вернёт нам List<Dictionary<string, string>> со всеми комбинациями.
            List<Dictionary<string, string>> allCombinations = GetAllCombinations(item.Attributes);

            foreach (var combination in allCombinations)
            {
                var productVariant = new ProductVariant()
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    CreatedByUserId = createdByUserId
                };

                foreach (var pair in combination)
                {
                    // Получаем атрибут категории по имени ("Память", "Цвет" и т.д.)
                    CategoryAttribute categoryAttribute =  categoryAttributes
                        .Where(ca => ca.Name == pair.Key && ca.CategoryId == categoryId)
                        .First();
                    
                    CategoryAttributeValue categoryAttributeValue = categoryAttributeValues
                        .Where(cav => cav.Key == categoryAttribute.Id)
                        .First().Value //Получили список List<CategoryAttributeValue> (256GB, 512GB и т.д.)
                        .Where(cavv => cavv.Value == pair.Value)
                        .First();
                    
                    productVariantAttributeValues.Add(new ProductVariantAttributeValue()
                    {
                        Id = Guid.NewGuid(),
                        ProductVariantId = productVariant.Id,
                        CategoryAttributeValueId = categoryAttributeValue.Id,
                        CategoryAttributeId = categoryAttribute.Id,
                    });
                }
                    
                productVariants.Add(productVariant);
            }
            
            modelBuilder.Entity<ProductVariant>().HasData(productVariants);
            modelBuilder.Entity<ProductVariantAttributeValue>().HasData(productVariantAttributeValues);
            
            productsWithVariants.Add((product,  productVariants));
        }
        
        modelBuilder.Entity<Product>().HasData(productsWithVariants.Select(pv => pv.product));

       
        
        return productsWithVariants;
    }
    
       /// <summary>
    /// Возвращает список всех комбинаций (декартово произведение) в виде:
    ///   List&lt;Dictionary&lt;string, string&gt;&gt;,
    /// где в каждом Dictionary "Название атрибута" -> "Выбранное значение".
    /// </summary>
    private static List<Dictionary<string, string>> GetAllCombinations(Dictionary<string, string[]> attributes)
    {
        // 1) Получаем список ключей (атрибутов). 
        //    ToList() зафиксирует порядок, в котором мы будем проходить.
        var attributeKeys = attributes.Keys.ToList();

        // 2) Для каждого атрибута берём массив вариантов значений
        //    В том же порядке, что и Keys.
        var setsOfValues = attributeKeys
            .Select(key => attributes[key]) // string[] 
            .ToList();                      // List<string[]> 

        // 3) Делаем декартово произведение этих наборов
        //    Результат: IEnumerable<string[]> – каждый string[] содержит по 1 выбранному значению из каждого атрибута
        var cartesianResult = CartesianProduct(setsOfValues);

        // 4) Превращаем каждую комбинацию (string[]) в словарь "атрибут -> значение"
        var listOfDicts = new List<Dictionary<string, string>>();
        foreach (var comboArray in cartesianResult)
        {
            var dict = new Dictionary<string, string>();
            for (int i = 0; i < comboArray.Length; i++)
            {
                var attrName = attributeKeys[i];
                var attrValue = comboArray[i];
                dict[attrName] = attrValue;
            }
            listOfDicts.Add(dict);
        }

        return listOfDicts;
    }

    /// <summary>
    /// Декартово произведение произвольного количества массивов строк.
    /// Например, если на вход:
    ///   [ ["A","B"], ["X","Y"], ["1","2"] ]
    /// на выходе будет:
    ///   [ ["A","X","1"], ["A","X","2"], ["A","Y","1"], ["A","Y","2"], 
    ///     ["B","X","1"], ["B","X","2"], ["B","Y","1"], ["B","Y","2"] ].
    /// </summary>
    private static IEnumerable<string[]> CartesianProduct(List<string[]> sequences)
    {
        // Начинаем с набора из одного "пустого" массива
        IEnumerable<string[]> result = new[] { Array.Empty<string>() };

        // Последовательно "умножаем" результат на каждый массив (sequence)
        foreach (var sequence in sequences)
        {
            result =
                from partialCombo in result
                from element in sequence
                select partialCombo.Concat(new[] { element }).ToArray();
        }

        return result;
    }
}