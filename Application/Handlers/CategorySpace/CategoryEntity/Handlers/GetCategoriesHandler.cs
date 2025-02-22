using Application.Handlers.CategorySpace.CategoryEntity.DTOs;
using Application.Handlers.CategorySpace.CategoryEntity.Queries;  
using AutoMapper; 
using Domain.Interfaces.Repositories.CategorySpace; 
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;  
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Application.Handlers.CategorySpace.CategoryEntity.Responses;
using System.Text.Json;

namespace Application.Handlers.CategorySpace.CategoryEntity.Handlers
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, GetCategoryResponse>
    { 
        private readonly IMapper _mapper; 
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoriesHandler> _logger;
        private readonly IMemoryCache _cache;
        private readonly IWebHostEnvironment _env; // Добавляем IWebHostEnvironment

        public GetCategoriesHandler(
          ICategoryRepository categoryRepository,
          IMemoryCache cache,
          IMapper mapper,
          ILogger<GetCategoriesHandler> logger,
          IWebHostEnvironment env)
        {
        
            _categoryRepository = categoryRepository;
            _cache = cache;
            _mapper = mapper;
            _logger = logger;
            _env = env;

        }

        public async Task<GetCategoryResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        { 
            // Если среда разработки, просто выполняем запрос без кэширования
            if (_env.IsDevelopment())
            {
                _logger.LogWarning("⚠️ Кэширование отключено в режиме разработки!");

                var categories = await _categoryRepository.GetParentCategoriesAsync(cancellationToken); 

                var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

                var _categoriesJson = JsonSerializer.Serialize(categoriesDto, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return new GetCategoryResponse()
                {
                    Categories = _categoriesJson,
                };
            }


            var categoriesJson = await _cache.GetOrCreateAsync($"categories_cache_{CultureInfo.CurrentUICulture.TwoLetterISOLanguageName}", async entry =>
            {
               
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1); // Кэшируем на 1 час

                var categories = await _categoryRepository.GetParentCategoriesAsync(cancellationToken);
        
                var categoriesDto = _mapper.Map<List<CategoryDto>>(categories); // Используем AutoMapper
 

                return JsonSerializer.Serialize(categoriesDto, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }) ; // Кэшируем сериализованный JSON
                 

            }) ?? throw new Exception("Не удалось создать дерево категорий");


            return new GetCategoryResponse()
            {
                Categories = categoriesJson,
            };
             
             
        }

 
 
    }
}
