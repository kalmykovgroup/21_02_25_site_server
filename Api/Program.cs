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
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Collections.Generic;

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
   
            
             var customTheme = new SystemConsoleTheme(new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle>
            {
                // Основной текст (по умолчанию белый/серый, чтобы не было черного)
                [ConsoleThemeStyle.Text] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Gray }, 

                // Дополнительный текст
                [ConsoleThemeStyle.SecondaryText] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray }, 
                [ConsoleThemeStyle.TertiaryText] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray }, 

                // Ошибки (Fatal и Error - делаем разными)
                [ConsoleThemeStyle.Invalid] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Red }, // ❌ ОШИБКА (красный текст)
                [ConsoleThemeStyle.LevelError] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Red }, // Красный уровень ERROR
                [ConsoleThemeStyle.LevelFatal] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkRed }, // Темно-красный FATAL

                // Предупреждения
                [ConsoleThemeStyle.LevelWarning] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Yellow }, // ⚠️ ПРЕДУПРЕЖДЕНИЕ (желтый)
                
                // Информационные
                [ConsoleThemeStyle.LevelInformation] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Green }, // ✅ Информация (зеленый)
                
                // Debug
                [ConsoleThemeStyle.LevelDebug] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Blue }, // 🟦 Debug
                
                // Прочие стили
                [ConsoleThemeStyle.Scalar] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White }, 
                [ConsoleThemeStyle.Number] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan },
                [ConsoleThemeStyle.Null] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Magenta },
                [ConsoleThemeStyle.String] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Green },
                [ConsoleThemeStyle.Boolean] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Blue },
                [ConsoleThemeStyle.Name] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan }
            });

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(
                    theme: customTheme,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
 
            // Добавление Serilog в качестве провайдера логирования
            builder.Host.UseSerilog();
            
            
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
