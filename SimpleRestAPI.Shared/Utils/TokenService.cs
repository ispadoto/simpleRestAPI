using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SimpleRestAPI.Shared.Shared.Utils;

namespace SimpleRestAPI.Shared.Utils
{
    public static class TokenService
    {
        public static string GenerateToken(dynamic user, List<Claim> claims = null)
        {
            var secret = AppSettings.TokenKey; 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            // Criar ClaimsIdentity com as claims fornecidas
            var claimsIdentity = new ClaimsIdentity(claims);

            // Configurar a descrição do token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost/login",
                Audience = "SimpleRestAPI",
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static ClaimsPrincipal ValidateToken(string token)
        {
            // TODO: Implementar a validação do token corretamente
            var secret = AppSettings.TokenKey;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var issuer = "https://localhost/login";

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = issuer,
                ValidAudience = "SimpleRestAPI",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true, 
                ClockSkew = TimeSpan.FromMinutes(5) 
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch (Exception ex)
            {
                // Lidar com exceções conforme necessário
                // Por exemplo, você pode lançar uma exceção personalizada ou registrar o erro
                throw new SecurityTokenException("Invalid token", ex);
            }
        }
    }
}

