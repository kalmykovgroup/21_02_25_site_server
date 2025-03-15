using Domain.Entities.CategorySpace;
using Domain.Interfaces.Repositories.CategorySpace;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.CategorySpace
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext, ILogger<BaseRepository> logger) : base(dbContext, logger)
        {
        }


        public async Task<IEnumerable<Category>> GetParentCategoriesAsync(CancellationToken cancellationToken = default)
        {
            Category? main = await _dbContext.Categories
                .Where(c => !c.IsDeleted && c.Level == 1)
                .Include(c => c.SubCategories) // Подтягиваем подкатегории
                .OrderBy(c => c.Index) // Сортировка по полю Index  
                .FirstOrDefaultAsync(cancellationToken);

            if (main == null)
            {
                _logger.LogWarning("No main category found.");
            }

            return main?.SubCategories ?? new List<Category>();
        }
        
        public async Task<List<Guid>> GetCategoryIdsAndSubcategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            // Загружаем из базы только необходимые поля для повышения производительности
            var allCategories = await _dbContext.Categories
                .Select(c => new { c.Id, c.ParentCategoryId })
                .ToListAsync(cancellationToken);

            // Создаем словарь для быстрого доступа по Id
            var categoryDictionary = allCategories.ToDictionary(c => c.Id);

            // Строим lookup, где ключ – ParentCategoryId, а значение – список Id дочерних категорий.
            // Здесь автоматически учитываются только те категории, у которых ParentCategoryId не null (то есть, не корневые)
            var lookup = allCategories
                .Where(c => c.ParentCategoryId.HasValue) // Фильтруем только те, у которых ParentCategoryId не null
                .GroupBy(c => c.ParentCategoryId) // Группируем по ParentCategoryId (Guid?)
                .ToDictionary(g => g.Key!.Value, g => g.Select(c => c.Id).ToList());

            var result = new List<Guid>();

            // Рекурсивный метод для добавления Id категории и всех её подкатегорий в список
            void AddCategoryAndChildren(Guid id)
            {
                result.Add(id);
                if (lookup.TryGetValue(id, out var children))
                {
                    foreach (var childId in children)
                    {
                        AddCategoryAndChildren(childId);
                    }
                }
            }

            // Если категория с заданным Id существует (в том числе и корневая, когда ParentCategoryId == null),
            // начинаем рекурсивный обход.
            if (categoryDictionary.ContainsKey(categoryId))
            {
                AddCategoryAndChildren(categoryId);
            }

            return result;
        }
         
    }
}
