using Api.Middleware;
using Application.Common.Interfaces;
using Application.Handlers.CategorySpace.CategoryEntity.Mapping; 
using Application.Handlers.ProductSpace.ProductEntity.Mapping;
using Domain.Interfaces.Repositories.CategorySpace;
using Domain.Interfaces.Repositories.IntermediateSpace;
using Domain.Interfaces.Repositories.ProductSpace;
using Domain.Interfaces.Repositories.UserSpace;
using FluentValidation.AspNetCore;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories.CategorySpace;
using Infrastructure.Repositories.IntermediateSpace;
using Infrastructure.Repositories.ProductSpace;
using Infrastructure.Repositories.UserSpace;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.Handlers.ProductSpace.WishListEntity.Commands;
using Application.Handlers.ProductSpace.ProductEntity.Handlers;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Автоматически загружаем настройки JWT в DI-контейнер
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);


            // Регистрация репозиториев
            builder.Services.AddScoped<IWishListProductRepository, WishListProductRepository>();

            builder.Services.AddScoped<IWishListRepository, WishListRepository>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IPhoneVerificationCodeRepository, PhoneVerificationCodeRepository>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


            // Регистрация сервиса для JWT
            builder.Services.AddSingleton<ITokenService, TokenService>();


            // Загружаем настройки JWT
            JwtSettings? jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

            // Проверяем длину ключа (должно быть минимум 64 символа)
            if (string.IsNullOrWhiteSpace(jwtSettings?.Key) || Encoding.UTF8.GetBytes(jwtSettings.Key).Length < 64)
            {
                throw new InvalidOperationException("JWT Secret Key is too short! It must be at least 64 characters for HS512.");
            }


            // Настройка аутентификации JWT
            builder.Services.AddAuthentication(options =>
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



            // Подключение Mapper 
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);


            // ✅ Добавить автоматическую регистрацию валидаторов
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            // ✅ Регистрируем FluentValidation Middleware (если используется)
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

            builder.Services.AddValidatorsFromAssembly(typeof(AddWishListProductCommand.Validator).Assembly);

            // ✅ Регистрируем MediatR           
            // ✅ Автоматическая регистрация всех `Handlers`, `Queries`, `Commands`
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetFilteredProductsHandler).Assembly));


            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();



            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });


            builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseLazyLoadingProxies().UseNpgsql(
                   builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection не настроен в appsettings.json.")
                )
            );

            builder.Services.AddScoped<IUnitOfWork, AppDbContext>();

            var PolicyName = "AllowAll";
            // 1. Добавляем CORS-сервис
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddMemoryCache(); 


            var app = builder.Build();

            app.UseCors(PolicyName);

            /* if (app.Environment.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
             }*/

            app.UseMiddleware<ExceptionMiddleware>();



            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Удаляем базу данных, если она существует
               // context.Database.EnsureDeleted();

                // Создаём базу данных заново
                context.Database.EnsureCreated();
            }
             

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>(); // ✅ JWT аутентификация. Защита от CSRF уже включена 

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
