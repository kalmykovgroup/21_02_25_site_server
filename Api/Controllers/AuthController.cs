using Application.Handlers.UserSpace.CustomerEntity.Commands;
using Application.Handlers.UserSpace.CustomerEntity.Responses; 
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        /// <summary>
        /// Запрос кода подтверждения на телефон.
        /// </summary>
        /// <param name="command">Номер телефона</param>
        /// <returns>Статус отправки кода</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            LoginResponse response = await _mediator.Send(command);

            if (!response.Success)
            {
                _logger.LogWarning(response.Error);

                return Ok(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Верификация кода и авторизация пользователя.
        /// Если пользователя с таким номером нет – выполняется авто-регистрация.
        /// </summary>
        /// <param name="command">Номер телефона и введённый код</param>
        /// <returns>JWT-токен и данные пользователя</returns>
        [HttpPost("verify-phone-code")]
        public async Task<IActionResult> VerifyPhoneCode([FromBody] VerifyPhoneCodeCommand command)
        {
            VerifyPhoneCodeResponse response = await _mediator.Send(command);

            if (!response.Success)
            {
                return Ok(response);
            }

            // ✅ Проверяем, является ли клиент Android (используем заголовок "X-Client-Type")
            bool isAndroidClient = Request.Headers.ContainsKey("X-Client-Type") &&
                                   Request.Headers["X-Client-Type"].ToString().ToLower() == "android";

            if (!isAndroidClient)
            {
                // ✅ Если это браузер, записываем токен в `Cookie`
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,  // ❌ Нельзя прочитать через JavaScript (защита от XSS)
                    Secure = true,    // ✅ Только по HTTPS
                    SameSite = SameSiteMode.Strict, // ✅ Защита от CSRF
                    Expires = DateTime.UtcNow.AddMinutes(60) // Время жизни токена
                };

                Response.Cookies.Append("jwt", response.Token!, cookieOptions);

                response.Token = null;
            }
           
            _logger.LogInformation("\n Пользователь успешно выполнил вход! \n");
             
            return Ok(response);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Logged out" });
        }


     
    }
}
