using System.Text;
using backend.Services.Interfaces;
using backend.Dto.Token;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using backend.Models;
using backend.Dto.Trainee;

namespace backend.Services.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext _context;
        private readonly string _secretKey;
        private readonly int _refreshTokenLength = 32;
        private readonly int _refreshTokenDurationDays = 90;
        private readonly int _accessTokenDurationMinutes = 1;

        public TokenRepository(
            AppDbContext context,
            IConfiguration configuration
        )
        {
            _context = context;
            var secretKey = configuration["AppSettings:SecretKey"];
            _secretKey = secretKey != null ? secretKey : "";
        }

        public string GenerateAccessTokenString(
            int userId,
            string firstName,
            string username,
            SystemRole systemRole
        )
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_secretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, firstName),
                        new Claim(ClaimTypes.NameIdentifier, username),
                        new Claim("Id", userId.ToString()),
                        new Claim(ClaimTypes.Role, systemRole.ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddMinutes(
                    _accessTokenDurationMinutes
                ),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }

        public string GenerateRefreshTokenString()
        {
            var random = new byte[_refreshTokenLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }

        public RefreshTokenDto? GetRefreshTokenById(int refreshTokenId)
        {
            var refreshToken = _context.RefreshTokens.SingleOrDefault(
                t => t.Id == refreshTokenId
            );

            return refreshToken != null
                ? Utils.ConvertRefreshTokenToDto(refreshToken)
                : null;
        }

        public RefreshTokenDto CreateRefreshToken(string refreshToken)
        {
            var newRefreshToken = new RefreshToken()
            {
                Token = refreshToken,
                CreatedTime = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddDays(_refreshTokenDurationDays)
            };

            _context.RefreshTokens.Add(newRefreshToken);
            _context.SaveChanges();

            return Utils.ConvertRefreshTokenToDto(newRefreshToken);
        }

        public int RenewRefreshToken(int tokenId, string token)
        {
            var refreshToken = _context.RefreshTokens.SingleOrDefault(
                t => t.Id == tokenId
            );

            if (refreshToken == null)
            {
                return 1;
            }

            refreshToken.Token = token;
            refreshToken.CreatedTime = DateTime.UtcNow;
            refreshToken.ExpiryTime = DateTime.UtcNow.AddDays(
                _refreshTokenDurationDays
            );

            _context.RefreshTokens.Update(refreshToken);
            _context.SaveChanges();

            return 0;
        }
    }
}
