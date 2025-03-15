using AutoMapper;
using Domain.Entities.CategorySpace;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbInitializer;

public static class DefaultCategories
{

    public static List<Category> CreateDefaultCategories(this ModelBuilder modelBuilder, IMapper mapper,
        Guid createdByUserId)
    {
        // Создаем категории
        var categories = AppDbContextSeed.FlattenCategories(AppDbContextSeed.SeedCategoriesAsync(), mapper);


        foreach (var category in categories)
        {
            category.CreatedByUserId = createdByUserId;
        }


        modelBuilder.Entity<Category>().HasData(categories);

        return categories;
    }
}