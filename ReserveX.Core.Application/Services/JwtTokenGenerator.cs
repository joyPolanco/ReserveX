

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReserveX.Core.Application.Interfaces;
using ReserveX.Core.Domain.Entities;
using ReserveX.Core.Domain.Settings;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ReserveX.Core.Application.Services
{
    public class JwtTokenGenerator: IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> options)
        {
            _jwtSettings= options.Value;
        }
        public string GenerateAccessToken(Guid userId, string email, string role)
        {
            var userRole = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(role.ToString().ToLower());


            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim (ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                 expires: DateTime.Now.AddMinutes(_jwtSettings.LifeTimeInMinutes),
                 signingCredentials: creds);
                  
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
