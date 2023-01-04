using System.Security.Cryptography;
using System.Text;
using backend.Dto.AdminUser;
using backend.Dto.Token;
using backend.Dto.Trainee;
using backend.Models;

namespace backend.Services.Repositories
{
    internal class Utils
    {
        public static readonly int SALT_LENGTH = 32;

        public static TraineeDto ConvertTraineeToDto(Trainee trainee)
        {
            return new TraineeDto()
            {
                Id = trainee.Id,
                FirstName = trainee.FirstName,
                LastName = trainee.LastName,
                Level = trainee.Level,
                SystemRole = trainee.SystemRole,
                ImgLink = trainee.ImgLink,
                Username = trainee.Username,
                RoleId = trainee.RoleId,
                DepartmentId = trainee.DepartmentId,
                RefreshTokenId = trainee.TokenId
            };
        }

        public static RefreshTokenDto ConvertRefreshTokenToDto(
            RefreshToken refreshToken
        )
        {
            return new RefreshTokenDto()
            {
                Id = refreshToken.Id,
                Token = refreshToken.Token,
                ExpiryTime = refreshToken.ExpiryTime,
                CreatedTime = refreshToken.CreatedTime,
            };
        }

        public static AdminUserDto ConvertAdminUserToDto(AdminUser adminUser)
        {
            return new AdminUserDto()
            {
                Id = adminUser.Id,
                FirstName = adminUser.FirstName,
                LastName = adminUser.LastName,
                Username = adminUser.Username,
                ImgLink = adminUser.ImgLink,
                SystemRole = adminUser.SystemRole,
                RefreshTokenId = adminUser.RefreshTokenId
            };
        }

        public static string GenerateSalt(int length)
        {
            var saltBytes = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        public static string HashPassword(string password, string salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var hmac = new HMACSHA512(saltBytes))
            {
                return Encoding.UTF8.GetString(hmac.ComputeHash(passwordBytes));
            }
        }
    }
}
