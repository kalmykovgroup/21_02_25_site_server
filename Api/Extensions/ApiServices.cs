using System.Text.Json;
using Api.Conventions;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Api.Extensions;

public static class ApiServices
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
        
        services.AddControllers(options =>
        {
            options.Conventions.Add(new GlobalRoutePrefixConvention("api"));
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
}