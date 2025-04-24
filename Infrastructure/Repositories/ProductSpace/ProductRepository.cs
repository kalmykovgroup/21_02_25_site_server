using Domain.Entities.ProductSpace; 
using Domain.Interfaces.Repositories.ProductSpace;
using Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 
using Application.Common.Interfaces; 

namespace Infrastructure.Repositories.ProductSpace
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly ILogger<BaseRepository> _logger;
        private readonly ICategoryCacheService _categoryCacheService;
        public ProductRepository(AppDbContext dbContext, ICategoryCacheService categoryCacheService, ILogger<BaseRepository> logger) : base(dbContext, logger)
        {
            _logger = logger;
            _categoryCacheService = categoryCacheService;
        }
        
    

 
     public async Task<(IEnumerable<SellerOffer> SellerOffers, bool HasMore)> GetAllSellerOffersAsync(
    string? search,
    Guid? categoryId,
    int page,
    int firstPageSize,
    int nextPageSize,
    CancellationToken cancellationToken = default)
{
    // 1️⃣ Определяем размер текущей страницы
    int pageSize = (page == 1) ? firstPageSize : nextPageSize;

    // 2️⃣ Узнаём, какие категории (включая подкатегории) нам нужны
    List<Guid>? categoryIds = null;
    if (categoryId.HasValue)
    {
        categoryIds = await _categoryCacheService.GetCategoryIdsAndSubcategoriesAsync(categoryId.Value);
    }

    // 3️⃣ Базовый запрос к SellerOffers
    //    - Включаем (Include) связанные сущности (Seller, ProductVariant→Product),
    //      чтобы сразу иметь доступ к названию товара, имени продавца и т.д.
    IQueryable<SellerOffer> query = _dbContext.SellerOffers
        .Where(so => so.IsActive) // Например, показываем только активные офферы
        .Include(so => so.Images);
        //.Include(so => so.Seller)
        /*.Include(so => so.ProductVariant)
            .ThenInclude(pv => pv.Product);*/

    // 4️⃣ Фильтр по категории (если указана)
    //    - Проверяем, чтобы Product.CategoryId входил в список {categoryId + подкатегории}
    if (categoryIds != null && categoryIds.Count > 0)
    {
        query = query.Where(so => categoryIds.Contains(so.ProductVariant.Product.CategoryId));
    }

    // 5️⃣ Фильтр по поисковому запросу (простейший вариант)
    if (!string.IsNullOrWhiteSpace(search))
    {
        string lowerSearch = search.Trim().ToLower();
        // К примеру, ищем совпадение в названии продукта или в имени продавца
        query = query.Where(so =>
            so.ProductVariant.Product.Name.ToLower().Contains(lowerSearch)
            || so.Seller.Name.ToLower().Contains(lowerSearch)
        );
    }

    // 6️⃣ Сортировка (здесь условно — по дате создания оффера, если в базе есть CreatedAt от AuditableEntity)
    //    Замените на нужное поле, если CreatedAt или что-то другое
    query = query.OrderBy(so => so.CreatedAt);

    // 7️⃣ Пагинация
    //    Для второй и последующих страниц нужно сдвигать "offset" с учётом того,
    //    что на первой странице у нас firstPageSize, а дальше — nextPageSize.
    int offset = (page - 1) * nextPageSize
                 + (page == 1 ? 0 : firstPageSize - nextPageSize);

    // 8️⃣ Получаем нужный блок офферов
    List<SellerOffer> sellerOffers = await query
        .Skip(offset)
        .Take(pageSize)
        .ToListAsync(cancellationToken);

    // 9️⃣ Проверяем, остались ли ещё офферы за текущей страницей
    bool hasMore = await query
        .Skip(offset + pageSize)
        .AnyAsync(cancellationToken);

    return (sellerOffers, hasMore);
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





 
 
    }
}
