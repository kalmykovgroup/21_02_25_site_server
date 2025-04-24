using AutoMapper;
using Domain.Entities.CategorySpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultCategories
{

    public static List<Category> CreateDefaultCategories(this ModelBuilder modelBuilder, IMapper mapper,
        Guid createdByUserId, ILogger logger)
    {
        // Создаем категории
        var categories = AppDbContextSeed.FlattenCategories(AppDbContextSeed.SeedCategoriesAsync(), mapper);


        foreach (var category in categories)
        {
            category.CreatedByUserId = createdByUserId;
        }


        modelBuilder.Entity<Category>().HasData(categories);

        logger.LogInformation($"Created {categories.Count} Categories:"); 
        
        return categories;
    }
}