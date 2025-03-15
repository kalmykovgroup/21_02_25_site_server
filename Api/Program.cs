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
using Api.Extensions;
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
            
            //Api
            builder.Services.AddApiServices(builder.Configuration);
            
            //Application
            builder.Services.AddApplicationServices(builder.Configuration);
            
            //Infrastructure
            builder.Services.AddInfrastructureServices(builder.Configuration);
   
 
            var app = builder.Build();

            app.UseCors("AllowAll");

            /* if (app.Environment.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
             }*/

            app.UseMiddleware<ExceptionMiddleware>();



            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Удаляем базу данных, если она существует
                context.Database.EnsureDeleted();

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
