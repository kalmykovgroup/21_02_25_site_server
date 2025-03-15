
using System.Text.Json;
using Application.Common.Interfaces;
using Domain.Interfaces.Repositories.CategorySpace;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
// реализация гибридного кеширования (IMemoryCache + Redis)
namespace Infrastructure.Caching
{
    public class CategoryCacheService : ICategoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryCacheService> _logger;

        // Настройки для распределенного кэша (Redis)
        private readonly DistributedCacheEntryOptions _distributedCacheOptions = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(30)
        };

        // Настройки для локального кэша (MemoryCache)
        private readonly MemoryCacheEntryOptions _memoryCacheOptions = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };

        public CategoryCacheService(
            IMemoryCache memoryCache,
            IDistributedCache distributedCache,
            ICategoryRepository categoryRepository,
            ILogger<CategoryCacheService> logger)
        {
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

       public async Task<List<Guid>> GetCategoryIdsAndSubcategoriesAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            // Проверка наличия данных в локальном кэше
            if (_memoryCache.TryGetValue(categoryId, out List<Guid>? cachedIds) && cachedIds is not null)
            {
                _logger.LogDebug("Local cache hit for categoryId: {CategoryId}", categoryId);
                return cachedIds;
            }

            var redisKey = $"CategoryIds:{categoryId}";
            List<Guid>? categoryIds = null;

            try
            {
                // Попытка получить данные из Redis
                var redisData = await _distributedCache.GetStringAsync(redisKey, cancellationToken);
                if (!string.IsNullOrEmpty(redisData))
                {
                    categoryIds = JsonSerializer.Deserialize<List<Guid>>(redisData);
                    _logger.LogDebug("Поиск в кэше Redis для CategoryID: {CategoryId}", categoryId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка доступа к Redis для ключа {RedisKey}", redisKey);
                // При ошибке можно выбрать стратегию fallback:
                // 1. Логировать ошибку и продолжить загрузку из БД.
                // 2. Или использовать другой источник кэша (например, DistributedMemoryCache).
            }

            if (categoryIds is null)
            {
                _logger.LogDebug("Кеш не найден для CategoryID: {categoryId}. Загрузка из базы данных.", categoryId);
                // Загружаем из БД через репозиторий
                categoryIds = await _categoryRepository.GetCategoryIdsAndSubcategoriesAsync(categoryId, cancellationToken);

                // Попытка сохранить в Redis (если Redis доступен)
                try
                {
                    var serializedData = JsonSerializer.Serialize(categoryIds);
                    await _distributedCache.SetStringAsync(redisKey, serializedData, _distributedCacheOptions, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка сохранения данных в Redis для ключа {RedisKey}", redisKey);
                }
            }

            // Сохраняем результат в локальный кэш независимо от источника
            _memoryCache.Set(categoryId, categoryIds, _memoryCacheOptions);

            return categoryIds;
        }

    }
}
