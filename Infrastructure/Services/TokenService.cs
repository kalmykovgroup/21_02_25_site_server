using Application.Common.Interfaces; 
  
using System.Text;  
using System.Security.Claims;
using Domain.Entities.UserSpace;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Domain.Entities.ProductSpace;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace Infrastructure.Services
{
    public struct ClaimType
    {
        public const string Id = "id";
        public const string UserType = "user_type";
        public const string PhoneNumber = "phone_number";
        public const string WishListId = "wish_list_id";
        public const string Jti = "jti"; 
    }

    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;

            if (string.IsNullOrWhiteSpace(_jwtSettings.Key) || Encoding.UTF8.GetBytes(_jwtSettings.Key).Length < 64)
            {
                throw new InvalidOperationException("JWT Secret Key must be at least 64 characters long for HS512.");
            }
        }


        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimType.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimType.Id, user.Id.ToString()),
                new Claim(ClaimType.UserType, $"{user.UserType}"),
                new Claim(ClaimType.WishListId, $"{user.WishListId}"),
                new Claim(ClaimType.PhoneNumber, user.PhoneNumber)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        // ✅ Реализация метода ValidateToken
        public ClaimsPrincipal? ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // Убираем задержку при проверке токена
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch
            {
                return null; // Если токен невалиден, возвращаем null
            }
        }    
    }

    public static class ClaimsExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal User)
        {
            string? id = User.FindFirstValue(ClaimType.Id);

            if (id == null) return null;

            return Guid.Parse(id);
        }

        public static Guid? GetWishListId(this ClaimsPrincipal User)
        {
            string? id = User.FindFirstValue(ClaimType.WishListId);

            if (id == null) return null;

            return Guid.Parse(id);
        }

        public static string? GetUserPhoneNumber(this ClaimsPrincipal User)
        {
            return User.FindFirstValue(ClaimType.PhoneNumber);
        }

        public static string? GetUserJti(this ClaimsPrincipal User)
        {
            return User.FindFirstValue(ClaimType.Jti);
        }

        public static UserType? GetUserType(this ClaimsPrincipal User)
        {
            string? userType = User.FindFirstValue(ClaimType.UserType);

            if (userType == null) return null;

            return Enum.Parse<UserType>(userType);
        }
    }
}
