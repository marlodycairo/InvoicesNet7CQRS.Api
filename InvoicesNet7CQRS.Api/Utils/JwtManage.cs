using InvoicesNet7CQRS.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvoicesNet7CQRS.Api.Utils
{
    public class JwtManage
    {
        private readonly IConfiguration _config;

        public JwtManage(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<string> GenerateJsonWebToken(Login login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Username!),
                new Claim(JwtRegisteredClaimNames.Sub, login.Username!),
                new Claim(JwtRegisteredClaimNames.Aud, "https://localhost:7086"),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(5).ToString())
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(5),
                signingCredentials: credential);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
