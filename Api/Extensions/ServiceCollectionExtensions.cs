using System.Reflection;
using System.Text;
using System.Text.Json;
using Application.Common.Interfaces;
using Application.Handlers.CategorySpace.CategoryEntity.Mapping;
using Application.Handlers.ProductSpace.ProductEntity.Handlers;
using Application.Handlers.ProductSpace.ProductEntity.Mapping;
using Application.Handlers.ProductSpace.WishListEntity.Commands;
using Domain.Interfaces.Repositories.CategorySpace;
using Domain.Interfaces.Repositories.IntermediateSpace;
using Domain.Interfaces.Repositories.ProductSpace;
using Domain.Interfaces.Repositories.UserSpace;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Caching;
using Infrastructure.Data;
using Infrastructure.Repositories.CategorySpace;
using Infrastructure.Repositories.IntermediateSpace;
using Infrastructure.Repositories.ProductSpace;
using Infrastructure.Repositories.UserSpace;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens; 
namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Автоматически загружаем настройки JWT в DI-контейнер
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);
        
        // 1. Добавляем CORS-сервис
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddMemoryCache(); 
        
        services.AddAuthorization(); 
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                // Игнорирование циклических ссылок
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                
                // Использование camelCase для имен свойств
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            });
        services.AddEndpointsApiExplorer();
  
        return services;
    }
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        // ✅ Регистрируем MediatR           
        // ✅ Автоматическая регистрация всех `Handlers`, `Queries`, `Commands`
        // Регистрируйте все обработчики CQRS
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetFilteredProductsHandler).Assembly));
       
        
        // Подключение Mapper 
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(typeof(ProductProfile).Assembly);
        services.AddAutoMapper(typeof(CategoryProfile).Assembly);


        // ✅ Добавить автоматическую регистрацию валидаторов
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        // ✅ Регистрируем FluentValidation Middleware (если используется)
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssembly(typeof(AddWishListProductCommand.Validator).Assembly);
 
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Регистрация DbContext, репозиториев, кэш-сервисов и т.д.
        // Регистрация репозиториев
        services.AddScoped<IWishListProductRepository, WishListProductRepository>();

        services.AddScoped<IWishListRepository, WishListRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPhoneVerificationCodeRepository, PhoneVerificationCodeRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRecommendedGroupRepository, RecommendedGroupRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        
        // Регистрация сервиса для JWT
        services.AddSingleton<ITokenService, TokenService>();
        
        //Cache
        services.AddScoped<ICategoryCacheService, CategoryCacheService>();


        // Загружаем настройки JWT
        JwtSettings? jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

        // Проверяем длину ключа (должно быть минимум 64 символа)
        if (string.IsNullOrWhiteSpace(jwtSettings?.Key) || Encoding.UTF8.GetBytes(jwtSettings.Key).Length < 64)
        {
            throw new InvalidOperationException("JWT Secret Key is too short! It must be at least 64 characters for HS512.");
        }


        // Настройка аутентификации JWT
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseLazyLoadingProxies().UseNpgsql(
                configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection не настроен в appsettings.json.")
            )
        );

        services.AddScoped<IUnitOfWork, AppDbContext>();
        
        var redisConnection = configuration.GetConnectionString("Redis");

        if (string.IsNullOrEmpty(redisConnection))
        {
            // Если Redis не настроен, используем fallback – локальный распределённый кэш
            services.AddDistributedMemoryCache();
        }
        else
        {
            // Если Redis настроен, регистрируем его
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
            });
        }
         
        
        return services;
    }
}