using Domain.Entities.BrandSpace;
using Domain.Entities.CategorySpace;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultProducts
{
    
    private static readonly Random _random = new Random();
    
    public static  List<(Product product, List<ProductVariant> productVariants, List<ProductVariantAttributeValue> productVariantAttributeValues)> CreateDefaultProducts(
        this ModelBuilder modelBuilder, 
        (string CategoryName, string Name, string Brand, Dictionary<string, string[]> Attributes)[] productsConfigurations, 
        List<CategoryAttribute> categoryAttributes,
        Dictionary<Guid, List<CategoryAttributeValue>> categoryAttributeValues,
        Guid supplierId,
        List<Category> categories,
        List<Brand> brands,
        Guid createdByUserId,
        ILogger logger)
    {
        
        List<(Product product, List<ProductVariant> productVariants, List<ProductVariantAttributeValue> productVariantAttributeValues)> productsWithVariants = new ();
        
        // Создаем продукты, item -> { categoryName, name, brand, attributes }
        // "iPhone 16 Pro Max"
        foreach (var item in productsConfigurations)
        {
            
            Brand? brand = brands.FirstOrDefault(b => b.Name == item.Brand);

            if (brand == null)
            {
                throw new Exception($"Brand not found: {item.Brand}");
            }
            
            Category? categoryProduct = categories.Where(c => c.Name == item.CategoryName).FirstOrDefault();
            
            if (categoryProduct == null)
            {
                throw new Exception($"Category not found: {item.CategoryName}");
            }
            
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                CategoryId = categoryProduct.Id,
                BrandId = brand.Id,

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
                //combination = { { "Цвет", "Титановый белый" }, { "Память", "128GB" } }
                logger.LogInformation($"Combination: {string.Join(", ", combination.Select(c => $"{c.Key}: {c.Value}"))}");

            
                
                var productVariant = new ProductVariant()
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    CreatedByUserId = createdByUserId,
                    IsActive = true,
                };
                 

               //combination = { { "Цвет", "Титановый белый" }, { "Память", "128GB" } }
               //pair = { "Цвет", "Титановый белый" } 
                foreach (var pair in combination)
                {
                    
                    //Ранее мы создали CategoryAttributes и CategoryAttributeValues на основе списка продуктов
                    //Теперь мы можем получить из из списка, который передан в базу
                  
                    // Получаем атрибут категории по имени ("Память", "Цвет" и т.д.)
                    CategoryAttribute? categoryAttribute =  categoryAttributes
                        .Where(ca => ca.Name == pair.Key && ca.CategoryId == categoryProduct.Id)
                        .FirstOrDefault();
                    
                    if(categoryAttribute == null) throw new Exception($"Category attribute not found: {pair.Key}");
                    
                     
                    //Key - это categoryAttribute.Id , Value - это список значений атрибута (256GB, 512GB и т.д.)
                    
                    CategoryAttributeValue? categoryAttributeValue = categoryAttributeValues
                        .Where(cav => cav.Key == categoryAttribute.Id)
                        .First().Value //Получили список List<CategoryAttributeValue> (256GB, 512GB и т.д.)
                        .Where(cavv => cavv.Value == pair.Value)
                        .FirstOrDefault();
                    
                    
                    
                    if (categoryAttributeValue == null) throw new Exception($"Category attribute value not found: {pair.Value}");
                    
                    productVariantAttributeValues.Add(new ProductVariantAttributeValue()
                    {
                        Id = Guid.NewGuid(),
                        ProductVariantId = productVariant.Id,
                        CategoryAttributeValueId = categoryAttributeValue.Id,
                        CategoryAttributeId = categoryAttribute.Id,
                    });
                }
                
                productVariant.Sku = GenerateSku(categoryProduct.Name, brand.Name);
                productVariant.Barcode = GenerateBarcode();
                    
                productVariants.Add(productVariant);
            }
            
            modelBuilder.Entity<ProductVariant>().HasData(productVariants);
            modelBuilder.Entity<ProductVariantAttributeValue>().HasData(productVariantAttributeValues);
            
            productsWithVariants.Add((product,  productVariants, productVariantAttributeValues));
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
    
    /// <summary>
    /// Пример функции формирования SKU на базе названия, цвета, объёма памяти.
    /// Убираем пробелы/неугодные символы и соединяем.
    /// </summary>
    public static string GenerateSku(string category, string brand)
    {
        // Берем первые 3 буквы категории и бренда (если слово короче - добавляем)
        string categoryCode = (category.Length >= 3) ? category.Substring(0, 3).ToUpper() : category.ToUpper().PadRight(3, 'X');
        string brandCode = (brand.Length >= 3) ? brand.Substring(0, 3).ToUpper() : brand.ToUpper().PadRight(3, 'X');

        // Генерируем случайный код (6 символов: буквы + цифры)
        string randomCode = GenerateRandomString(6);

        // Формируем SKU
        return $"{categoryCode}-{brandCode}-{randomCode}";
    }
    
    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Range(0, length).Select(_ => chars[_random.Next(chars.Length)]).ToArray());
    }
    
    public static string GenerateBarcode()
    {
        // Генерируем первые 12 цифр случайно
        string barcodeWithoutChecksum = string.Concat(Enumerable.Range(0, 12).Select(_ => _random.Next(0, 10)));

        // Вычисляем контрольную цифру
        int checksum = CalculateEAN13Checksum(barcodeWithoutChecksum);

        // Полный штрих-код
        return barcodeWithoutChecksum + checksum;
    }

    private static int CalculateEAN13Checksum(string barcode)
    {
        int sum = 0;
        for (int i = 0; i < barcode.Length; i++)
        {
            int digit = barcode[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }
        int checksum = (10 - (sum % 10)) % 10;
        return checksum;
    }


}