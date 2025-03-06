using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories.ProductSpace;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ProductSpace
{
    public class RecommendedGroupRepository : BaseRepository , IRecommendedGroupRepository
    {
        public RecommendedGroupRepository(AppDbContext dbContext, ILogger<BaseRepository> logger) : base(dbContext, logger)
        {
        }


        public async Task<(IEnumerable<RecommendedGroup>, bool HasMore)> GetGroups(int page, int firstPageSize, int nextPageSize)
        {
            int pageSize = page == 1 ? firstPageSize : nextPageSize;
            int offset = (page - 1) * nextPageSize + (page == 1 ? 0 : firstPageSize - nextPageSize);

            // 🔹 Оптимизированный запрос: выбираем только группы, где ровно 4 товара
            var validGroupsQuery = _dbContext.RecommendedGroups
                .Where(rg => rg.RecommendedGroupProducts.Count() == 4) // ✅ Фильтруем по 4 продуктам
                .OrderBy(rg => rg.Title); // ✅ Добавляем сортировку

            // 🔹 Получаем данные с пагинацией
            var validGroups = await validGroupsQuery
                .Skip(offset)
                .Take(pageSize)
                .Include(rg => rg.RecommendedGroupProducts)
                    .ThenInclude(rgp => rgp.Product)
                .ToListAsync();

            // 🔹 Определяем, есть ли еще данные
            bool hasMore = await validGroupsQuery
                .Skip(offset + pageSize)
                .AnyAsync();

            return (validGroups, hasMore);
        }


    }
}
