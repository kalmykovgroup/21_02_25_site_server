using System.Security.Cryptography.X509Certificates;
using System.Text;
using Application.Common.Interfaces;
using Domain.Interfaces.Repositories.CategorySpace;
using Domain.Interfaces.Repositories.IntermediateSpace;
using Domain.Interfaces.Repositories.ProductSpace;
using Domain.Interfaces.Repositories.UserSpace;
using Infrastructure.Caching;
using Infrastructure.Data;
using Infrastructure.Repositories.CategorySpace;
using Infrastructure.Repositories.IntermediateSpace;
using Infrastructure.Repositories.ProductSpace;
using Infrastructure.Repositories.UserSpace;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class InfrastructureServices
{
      public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool isProduction)
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


        string GetRequiredEnv(string key) => Environment.GetEnvironmentVariable(key)
            ?? throw new InvalidOperationException($"Ожидалась переменная окружения '{key}', но она не задана.");


        string? connectionString;

        if (isProduction)
        {
            // В проде читаем из env-переменных и файла
            var db = GetRequiredEnv("POSTGRES_DB");
            var user = GetRequiredEnv("POSTGRES_USER");
            var passwordFile = GetRequiredEnv("POSTGRES_PASSWORD_FILE");
            var db_host = GetRequiredEnv("DB_HOST");
            var db_port = GetRequiredEnv("DB_PORT");

            if (!File.Exists(passwordFile)) throw new FileNotFoundException($"Файл с паролем не найден: {passwordFile}");

            var password = File.ReadAllText(passwordFile).Trim();

            connectionString = $"Host={db_host};Port={db_port};Database={db};Username={user};Password={password}";
            Console.WriteLine("🔐 Using production environment variables for DB connection.");
        }
        else
        {
            // В dev — из конфигурации
            connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Строка подключения не найдена в конфигурации.");
            Console.WriteLine("🧪 Using development config file for DB connection.");
        }

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));


     

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
        if (isProduction)
        {
            var certsDir = GetRequiredEnv("CERTS_DIR"); 
            var appName = GetRequiredEnv("APPLICATION_NAME");
            var cert_password_file = GetRequiredEnv("CERT_PASSWORD_FILE");

            var certPath = Path.Combine(certsDir, "cert.pem");
            var keyPath = Path.Combine(certsDir, "key.pem"); 

            if (!File.Exists(certPath) || !File.Exists(keyPath))
                throw new FileNotFoundException("PEM-файлы не найдены");

            var certPassword = File.ReadAllText(cert_password_file).Trim();
            var cert = X509Certificate2.CreateFromEncryptedPemFile(certPath, certPassword, keyPath);

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("/keys"))
                .ProtectKeysWithCertificate(cert)
                .SetApplicationName(appName);
        }


        return services;
    }
 
}