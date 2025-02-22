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
            return await _dbContext.Categories
                .Where(c => !c.IsDeleted && c.Level == 0)
                .Include(c => c.SubCategories) // Подтягиваем подкатегории
                    .ToListAsync(cancellationToken);
        }
         
    }
}
