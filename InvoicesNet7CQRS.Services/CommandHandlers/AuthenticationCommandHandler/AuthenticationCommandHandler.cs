using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Data.Interfaces;
using InvoicesNet7CQRS.Domain.Commands.AuthenticationCommands;
using InvoicesNet7CQRS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvoicesNet7CQRS.Services.CommandHandlers.AuthenticationCommandHandler
{
    public class AuthenticationCommandHandler
    {
        public class GetAuthenticationCommandHandler : IRequestHandler<GetAuthenticationQuery, GenericResponse<User>>
        {
            private readonly IConfiguration _config;
            private readonly IDbContext _context;

            public GetAuthenticationCommandHandler(IConfiguration configuration, IDbContext dbContext)
            {
                _config = configuration;
                _context = dbContext;
            }

            public async Task<GenericResponse<User>> Handle(GetAuthenticationQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<User>();

                if (await IsUserValid(request))
                {
                    var token = await GenerateJsonWebToken(request);

                    response.Message = $"User authenticated: {token}";

                    return response;
                }

                response.Message = "User unauthorized!";

                return response;
            }

            private async Task<bool> IsUserValid(GetAuthenticationQuery query)
            {
                var users = await _context.Users.Where(x => x.Username == query.Username).ToListAsync();

                return users!.Any(x => x.Username == query.Username && x.Pass == query.Pass);
            }

            private async Task<string> GenerateJsonWebToken(GetAuthenticationQuery query)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

                var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, query.Username!),
                new Claim(JwtRegisteredClaimNames.Sub, query.Username!),
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
}
