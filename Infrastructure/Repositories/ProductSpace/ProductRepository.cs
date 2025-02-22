using Domain.Entities.ProductSpace; 
using Domain.Interfaces.Repositories.ProductSpace;
using Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ProductSpace
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext, ILogger<BaseRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<int> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

            if (existingProduct == null)
            {
                return -1; // ❌ Продукт не найден
            }

            // ✅ Обновляем только измененные поля
            _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);

            return await _dbContext.SaveChangesAsync(cancellationToken); 
        }



        public async Task<(IEnumerable<Product> Products, bool HasMore)> GetAllProductsAsync(
        string? search,
        Guid? categoryId,
        int page,
        CancellationToken cancellationToken = default)
        {
            int firstPageSize = 30; // Количество товаров на первой странице
            int nextPageSize = 20;  // Количество товаров на остальных страницах
            int pageSize = page == 1 ? firstPageSize : nextPageSize; // Определяем, какая страница

            // 1️⃣ Получаем список категорий (включая дочерние)
            List<Guid> categoryIds = new();
            if (categoryId.HasValue)
            {
                categoryIds = await _dbContext.Categories
                    .Where(c => c.Id == categoryId.Value || c.ParentCategoryId == categoryId.Value)
                    .Select(c => c.Id)
                    .ToListAsync(cancellationToken);
            }

            // 2️⃣ Базовый запрос к БД
            IQueryable<Product> query = _dbContext.Products
                .Where(p => !p.IsDeleted) // Исключаем удалённые продукты
                .AsQueryable();

            // 3️⃣ Фильтр по категории (если указана)
            if (categoryIds.Any())
            {
                query = query.Where(p => categoryIds.Contains(p.CategoryId));
            }

            // 4️⃣ Фильтр по поисковому запросу
            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearch));
            }

            // 5️⃣ Сортировка (по названию)
            query = query.OrderBy(p => p.Name);

            // 6️⃣ Пагинация
            List<Product> products = await query
                .Skip((page - 1) * nextPageSize + (page == 1 ? 0 : firstPageSize - nextPageSize))
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            // 7️⃣ Определяем, есть ли ещё данные
            bool hasMore = await query
                .Skip((page - 1) * nextPageSize + (page == 1 ? 0 : firstPageSize - nextPageSize) + pageSize)
                .AnyAsync(cancellationToken);

            return (products, hasMore);
        }





        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Продукт не может быть null");
            }

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return product; // ✅ Возвращаем добавленный продукт
        }

 
    }
}
