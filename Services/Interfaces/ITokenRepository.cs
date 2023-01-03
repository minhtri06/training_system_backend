using backend.Dto.Token;
using backend.Dto.Trainee;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface ITokenRepository
    {
        string GenerateAccessTokenString(
            int userId,
            string firstName,
            string username,
            SystemRole systemRole
        );
        string GenerateRefreshTokenString();
        RefreshTokenDto? GetRefreshTokenById(int refreshTokenId);
        RefreshTokenDto CreateRefreshToken(string refreshToken);
        int RenewRefreshToken(int tokenId, string refreshToken);
    }
}
