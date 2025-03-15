using Domain.Entities.CategorySpace;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultCategoryAttributes
{
    public static (List<CategoryAttribute> categoryAttributes, Dictionary<Guid, List<CategoryAttributeValue>> categoryAttributeValues) CreateDefaultCategoryAttributes(
        this ModelBuilder modelBuilder,
        List<Category> categories,
        (string CategoryName, string Name, string Brand, Dictionary<string, string[]> Attributes)[] productsConfigurations,
        Guid createdByUserId)
    {
        var categoryAttributes = new List<CategoryAttribute>();
        
        //Guid это id categoryAttribute
        //Ключ - это id атрибута, значение - это список значений атрибута
        Dictionary<Guid, List<CategoryAttributeValue>> categoryAttributeValues = new Dictionary<Guid, List<CategoryAttributeValue>>();
         

        //Проход по продуктам (S24 Ultra, Iphone 16 pro max и т.д.) { categoryName, name, brand, attributes }
        foreach (var configuration in productsConfigurations)
        {
            // Получаем категорию по имени ("Мобильные телефоны")
            Category category = categories.Where(c => c.Name == configuration.CategoryName).First();
            
            // Проход по атрибутам продукта (color, storage и т.д.) ("Память", new[] { "128GB", "256GB", "512GB" })
            foreach (var attribute in configuration.Attributes)
            {
                
                // Получаем атрибут категории по имени ("Память", "Цвет" и т.д.)
                CategoryAttribute? categoryAttribute = categoryAttributes.Where(ca => ca.Name == attribute.Key && ca.CategoryId == category.Id).FirstOrDefault();

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
                }
                
                
                //Наполняем словарь значениями атрибутов
                //Ключ - это id атрибута, значение - это список значений атрибута
                if(categoryAttributeValues.TryGetValue(categoryAttribute.Id, out List<CategoryAttributeValue>? attributeValues))
                {
                    if(attributeValues == null) throw new Exception("Attribute values list is null");
                    
                    foreach (var value in attribute.Value)
                    {
                        if (!attributeValues.Any(av => av.Value == value))
                        {
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
                    List<CategoryAttributeValue> newAttributeValues = new List<CategoryAttributeValue>();
                    
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
                    
                    categoryAttributeValues.Add(categoryAttribute.Id, newAttributeValues);
                }
                
              
            }
        }
        
        
        List<CategoryAttributeValue> allAttributeValues = categoryAttributeValues
            .Values            // это коллекция List<CategoryAttributeValue>, которые хранятся в словаре
            .SelectMany(v => v) // "раскрываем" все списки в единую последовательность
            .ToList();         // формируем итоговый список
        
       
        modelBuilder.Entity<CategoryAttributeValue>().HasData(allAttributeValues);
        
        modelBuilder.Entity<CategoryAttribute>().HasData(categoryAttributes);

        return (categoryAttributes, categoryAttributeValues);
    }
}