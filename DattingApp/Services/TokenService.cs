using DattingAppApi.Entities;
using DattingAppApi.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DattingAppApi.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public string CreateToken(AppUser user)
        {
            string tokenKey = configuration["TokenKey"] ?? throw new Exception("No TokenKey has been provided at appsettings.json");
            if (tokenKey.Length < 64) throw new Exception("TokenKey needs to be 64 chars long");
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim> 
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
