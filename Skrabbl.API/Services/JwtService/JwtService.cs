using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public class JwtService : IJwtService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly string _secret;
        private readonly string _expDate;

        public JwtService(IConfiguration config, ITokenRepository tokenRepository)
        {
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
            _tokenRepository = tokenRepository;
        }

        public JwtToken GenerateSecurityToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var expiresAt = DateTime.UtcNow.AddMinutes(double.Parse(_expDate));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtToken
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresAt = expiresAt
            };
        }

        private static string GenerateRefreshToken()
        {
            using var rng = RandomNumberGenerator.Create();

            var bytes = new byte[32];
            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

        public async Task<RefreshToken> GenerateRefreshToken(User user)
        {
            var token = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                User = user
            };

            await _tokenRepository.AddRefreshToken(token);

            return token;
        }

        public Task<bool> RemoveToken(string refreshToken)
        {
            return _tokenRepository.RemoveRefreshToken(refreshToken);
        }

        public async Task<RefreshToken> RefreshToken(User user, string refreshToken)
        {
            var deletedToken = await _tokenRepository.RemoveRefreshToken(refreshToken);
            if (deletedToken)
            {
                return await GenerateRefreshToken(user);
            }

            return null;
        }
    }
}