using Domain.Entities.CategorySpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultCategoryAttributes
{
    public static (List<CategoryAttribute> categoryAttributes, Dictionary<Guid, List<CategoryAttributeValue>> categoryAttributeValues) CreateDefaultCategoryAttributes(
        this ModelBuilder modelBuilder,
        List<Category> categories,
        (string CategoryName, string Name, string Brand, Dictionary<string, string[]> Attributes)[] productsConfigurations,
        Guid createdByUserId, ILogger logger)
    {
        var categoryAttributes = new List<CategoryAttribute>();
        
        //Guid это id categoryAttribute
        //Key - это categoryAttribute.Id , Value - это список значений атрибута
        Dictionary<Guid, List<CategoryAttributeValue>> categoryAttributeValues = new Dictionary<Guid, List<CategoryAttributeValue>>();
         

        //Проход по продуктам (S24 Ultra, Iphone 16 pro max и т.д.) { categoryName, name, brand, attributes }
        foreach (var configuration in productsConfigurations)
        {
            // Получаем категорию по имени ("Мобильные телефоны")
            Category? category = categories.Where(c => c.Name == configuration.CategoryName).FirstOrDefault();
            
            if (category == null) throw new Exception($"Category not found: {configuration.CategoryName}");

            // logger.LogInformation($"Category: name: {category.Name} id: {category.Id}");

            // Проход по атрибутам продукта (color, storage и т.д.) ("Память", new[] { "128GB", "256GB", "512GB" })
            foreach (var attribute in configuration.Attributes)
            {
                
                // Получаем атрибут категории по имени ("Память", "Цвет" и т.д.) Важно что мы указываем на категорию, так как атрибут может повторяться в разных категориях!
                CategoryAttribute? categoryAttribute = categoryAttributes.Where(ca => ca.CategoryId == category.Id && ca.Name == attribute.Key).FirstOrDefault();
 
                // Если атрибут не существует для данной категории ("Мобильные телефоны"), то создаем его
                if (categoryAttribute == null)
                {
                    categoryAttribute = new CategoryAttribute()
                    {
                        Id = Guid.NewGuid(),
                        Name = attribute.Key,
                        CategoryId = category.Id,
                        CreatedByUserId = createdByUserId,
                    };
                    
                    categoryAttributes.Add(categoryAttribute);
                     
                }
                
                
                
                //Наполняем словарь значениями атрибутов
                //Ключ - это id атрибута, значение - это список значений атрибута
                if(categoryAttributeValues.TryGetValue(categoryAttribute.Id, out List<CategoryAttributeValue>? attributeValues))
                {
                    if(attributeValues == null) throw new Exception("Attribute values list is null");
                    
                    // Если атрибут существует, то добавляем в него значения
                    foreach (var value in attribute.Value)
                    {
                        // Проверяем, существует ли значение в списке значений атрибута
                        if (!attributeValues.Any(av => av.Value == value))
                        {
                            // Если значение не существует, то добавляем его
                            attributeValues.Add(new CategoryAttributeValue()
                            {
                                Id = Guid.NewGuid(),
                                CategoryAttributeId = categoryAttribute.Id, 
                                Value = value,
                                CreatedByUserId = createdByUserId
                            });
                        }
                    }
                }
                else
                {
                    // Если атрибут не существует, то создаем его
                    List<CategoryAttributeValue> newAttributeValues = new List<CategoryAttributeValue>();
                    
                    // Добавляем значения атрибута в новый список
                    foreach (var value in attribute.Value)
                    {
                        newAttributeValues.Add(new CategoryAttributeValue()
                        {
                            Id = Guid.NewGuid(),
                            CategoryAttributeId = categoryAttribute.Id, 
                            Value = value,
                            CreatedByUserId = createdByUserId
                        });
                    }
                    // Добавляем новый список значений в словарь
                    categoryAttributeValues.Add(categoryAttribute.Id, newAttributeValues);
                }
                
              
            }
        }
        
      /*  logger.LogInformation($"CategoryAttributes:  {categoryAttributes.Count, -5}");
        
        foreach (var categoryAttribute in categoryAttributes)
        {
            logger.LogInformation($" " +
                                  $"-- Id: {categoryAttribute.Id, -30} " +
                                  $"-- Name: {categoryAttribute.Name, -10} " +
                                  $"- CategoryId: {categoryAttribute.CategoryId, -15} " +
                                  $"- Category.Name: {categories.Where(c => c.Id == categoryAttribute.CategoryId).FirstOrDefault()?.Name, -15}");
        }
        
        logger.LogInformation($"\n");
        logger.LogInformation($"CategoryAttributeValues: {categoryAttributeValues.Count, -5} ");

        foreach (var categoryAttributeValue in categoryAttributeValues)
        {
            logger.LogInformation(
                $" CateroryAttribute.Id: {categoryAttributeValue.Key, -70} " +
                $"CateroryAttribute.Name: {categoryAttributes.Where(c => c.Id == categoryAttributeValue.Key).FirstOrDefault()?.Name, -15} ");
            
            logger.LogInformation($" --- Values: {string.Join(", ", categoryAttributeValue.Value.Select(cav => cav.Value))}");
            logger.LogInformation($"\n");
        }
        logger.LogInformation($"\n");*/
        List<CategoryAttributeValue> allAttributeValues = categoryAttributeValues
            .Values            // это коллекция List<CategoryAttributeValue>, которые хранятся в словаре
            .SelectMany(v => v) // "раскрываем" все списки в единую последовательность
            .ToList();         // формируем итоговый список

     
       
        modelBuilder.Entity<CategoryAttributeValue>().HasData(allAttributeValues);
        
        modelBuilder.Entity<CategoryAttribute>().HasData(categoryAttributes);

        return (categoryAttributes, categoryAttributeValues);
    }
}