using Microsoft.IdentityModel.Tokens;
using SmartCart.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartCart.Identity.Services
{
    public class TokenGeneratingService : ITokenGeneratingService
    {
        public string? GenerateToken(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserID", user.UserID.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Fullname", user.FullName ?? ""),
                new Claim("BirthDate", user.BirthDate?.ToString("yyyy-MM-dd") ?? ""),
                new Claim("Username", user.Username),
                new Claim("IsNotificationEnabled", user.IsNotificationEnabled.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Audience = SD.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SD.JWTKey)),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = SD.Issuer
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            var jwtToken = handler.WriteToken(token);

            return jwtToken;
        }

        public string? GenerateToken(List<Claim> userInfo)
        {
            var claims = userInfo.Where(x => 
                x.Type == ClaimTypes.Email || 
                x.Type == ClaimTypes.Name);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Audience = SD.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SD.JWTKey)), 
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = SD.Issuer
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            var jwtToken = handler.WriteToken(token);

            return jwtToken;
        }
    }
}
