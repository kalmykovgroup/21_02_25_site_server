using Application.Common.Interfaces;

namespace Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public JwtMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = string.Empty;

            // ✅ 1. Проверяем заголовок Authorization
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                token = authHeader.ToString().Replace("Bearer ", "");
            }
            // ✅ 2. Проверяем Cookie (если токен хранится там)
            else if (context.Request.Cookies.TryGetValue("jwt", out var jwtCookie))
            {
                token = jwtCookie;
            }

            if (!string.IsNullOrEmpty(token))
            {
                var principal = _tokenService.ValidateToken(token);
                if (principal != null)
                {
                    context.User = principal; // ✅ Добавляем пользователя в HttpContext
                }
            }

            await _next(context);
        }
    }

}
