using Api.Middleware; 
using Infrastructure.Data; 
using Api.Extensions; 
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.AspNetCore.HttpOverrides;
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
            builder.Services.AddInfrastructureServices(builder.Configuration, !builder.Environment.IsDevelopment());
   
            
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

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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
                //  context.Database.EnsureDeleted();

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
