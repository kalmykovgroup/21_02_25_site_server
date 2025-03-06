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
        private readonly ILogger<BaseRepository> _logger;

        public ProductRepository(AppDbContext dbContext, ILogger<BaseRepository> logger) : base(dbContext, logger)
        {
            _logger = logger;
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
            int firstPageSize,
            int nextPageSize,
        CancellationToken cancellationToken = default)
        {  

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

            // 5️⃣ Сортировка (по дате добавления)
            query = query.OrderBy(p => p.CreatedAt);

            // 6️⃣ Пагинация
            int offset = (page - 1) * nextPageSize + (page == 1 ? 0 : firstPageSize - nextPageSize);


            // 6️⃣ Пагинация
            List<Product> products = await query
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            // 7️⃣ Определяем, есть ли ещё данные
            bool hasMore = await query
                .Skip(offset + pageSize)
                .AnyAsync(cancellationToken);

            return (products, hasMore);
        }


        public async Task<List<string>> GetProductNameSuggestionsAsync(string input, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<string>();

            // Формируем шаблон для ILIKE (регистронезависимый поиск)
            var pattern = "%" + input.Trim() + "%";

            // SQL-запрос для PostgreSQL (используем двойные кавычки для имен)
            var sqlQuery = $@"
        SELECT ""name""
        FROM ""products""
        WHERE ""is_active"" = true AND ""name"" ILIKE @pattern
        ORDER BY ""name""
        LIMIT {limit};";

            var suggestions = new List<string>();

            // Получаем соединение из контекста EF Core
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;

                    // Параметризация запроса для защиты от SQL-инъекций
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@pattern";
                    parameter.Value = pattern;
                    command.Parameters.Add(parameter);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            suggestions.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return suggestions;
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
