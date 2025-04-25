using System.Reflection;
using Application.Handlers.CategorySpace.CategoryEntity.Mapping;
using Application.Handlers.ProductSpace.ProductEntity.Handlers;
using Application.Handlers.ProductSpace.ProductEntity.Mapping;
using Application.Handlers.ProductSpace.WishListEntity.Commands;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Api.Extensions;

public static class ApplicationServices
{
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

}