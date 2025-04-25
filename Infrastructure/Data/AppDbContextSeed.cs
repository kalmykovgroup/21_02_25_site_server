using System.Text.Json;
using System.Text.RegularExpressions;
using AutoMapper;
using Domain.Entities.CategorySpace;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Data;

public static class AppDbContextSeed
{
     
    private const string CategoriesFileName = "extended_categories.json";

    public static List<Category> SeedCategoriesAsync()
    {
        List<Category> categories = new List<Category>();
        
        categories.Add(SeedCategoriesAsync(CategoriesFileName)); 
        
        return categories;
    }
    
    
    private static Category SeedCategoriesAsync(string fileName)
    {
        
        // Рекурсивно ищем файл в каталоге публикации
        var baseDirectory = AppContext.BaseDirectory;
        var filePath = Directory.GetFiles(baseDirectory, fileName, SearchOption.AllDirectories).FirstOrDefault();

        if (filePath == null)
            throw new FileNotFoundException($"❌ JSON-файл '{fileName}' не найден в каталоге {baseDirectory} и его подпапках.");

        Console.WriteLine($"✅ Найден файл: {filePath}");
        
        var jsonData = File.ReadAllText(filePath); 

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new Infrastructure.Serialization.GuidConverter() }
        };
 
        var rootCategory = JsonSerializer.Deserialize<Category>(jsonData, options);

        if (rootCategory == null)
        {
            throw new Exception($"Ошибка десериализации JSON {fileName}");
        }

  
        return rootCategory;

    }
    
    public static List<Category> FlattenCategories(List<Category> categories, IMapper mapper)
    {
        List<Category> flatList = new List<Category>();

        void Flatten(Category category)
        {
            // Клонируем категорию без навигационных свойств
            var flatCategory = mapper.Map<Category>(category);
            flatCategory.SubCategories = null!; // Убираем вложенные категории

            flatList.Add(flatCategory);

            if (category.SubCategories != null!)
            {
                foreach (var subCategory in category.SubCategories)
                {
                    Flatten(subCategory);
                }
            }
        }

        foreach (var category in categories)
        {
            Flatten(category);
        }

        return flatList;
    }



    
    public static void FindInvalidGuids(string filePath)
    {
        string jsonContent = File.ReadAllText(filePath);
        JObject jsonObject = JObject.Parse(jsonContent);

        List<string> invalidGuids = new List<string>();
        Regex guidRegex = new Regex(@"^[{(]?[0-9a-fA-F]{8}[-]?[0-9a-fA-F]{4}[-]?[0-9a-fA-F]{4}[-]?[0-9a-fA-F]{4}[-]?[0-9a-fA-F]{12}[)}]?$");

        void CheckGuids(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                foreach (var property in (JObject)token)
                {
                    if (property.Key == "Id" || property.Key == "ParentCategoryId")
                    {
                        string value = property.Value.ToString();
                        if (!guidRegex.IsMatch(value) && !string.IsNullOrEmpty(value))
                        {
                            invalidGuids.Add(value);
                        }
                    }
                    CheckGuids(property.Value);
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                foreach (var item in (JArray)token)
                {
                    CheckGuids(item);
                }
            }
        }

        CheckGuids(jsonObject);

        Console.WriteLine("Невалидные GUID:");
        foreach (var guid in invalidGuids)
        {
            Console.WriteLine($"{guid} -> {Guid.NewGuid()}");
        }
        
        
    }
}
    
