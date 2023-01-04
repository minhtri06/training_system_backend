using backend.Dto.AdminUser;
using backend.Dto.Login;
using backend.Models;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly AppDbContext _context;

        public AdminUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckUsernameExist(string username)
        {
            return _context.AdminUsers.Any(au => au.Username == username);
        }

        public AdminUserDto? GetById(int adminUserId)
        {
            var adminUser = _context.AdminUsers.SingleOrDefault(
                au => au.Id == adminUserId
            );

            return adminUser != null
                ? Utils.DtoConversion.ConvertAdminUser(adminUser)
                : null;
        }

        public AdminUserDto? GetByLoginInfo(LoginDto loginDto)
        {
            var adminUser = _context.AdminUsers.SingleOrDefault(
                au => au.Username == loginDto.Username
            );

            if (adminUser == null)
            {
                return null;
            }

            var checkedPasswordHash = Utils.Security.HashPassword(
                loginDto.Password,
                adminUser.PasswordSalt
            );

            if (checkedPasswordHash != adminUser.PasswordHash)
            {
                return null;
            }

            return Utils.DtoConversion.ConvertAdminUser(adminUser);
        }

        public ICollection<AdminUserDto> GetAll()
        {
            return _context.AdminUsers
                .Select(au => Utils.DtoConversion.ConvertAdminUser(au))
                .ToList();
        }

        public AdminUserDto Create(NewAdminUserDto newAdminUserDto)
        {
            var salt = Utils.Security.GenerateSalt(Utils.SALT_LENGTH);
            var passwordHash = Utils.Security.HashPassword(
                newAdminUserDto.Password,
                salt
            );

            var newAdminUser = new AdminUser()
            {
                FirstName = newAdminUserDto.FirstName,
                LastName = newAdminUserDto.LastName,
                SystemRole = SystemRole.Admin,
                ImgLink = newAdminUserDto.ImgLink,
                Username = newAdminUserDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
            };

            _context.AdminUsers.Add(newAdminUser);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertAdminUser(newAdminUser);
        }

        public AdminUserDto? DeleteById(int adminUserId)
        {
            var adminUser = _context.AdminUsers.SingleOrDefault(
                au => au.Id == adminUserId
            );

            if (adminUser == null)
            {
                return null;
            }

            if (adminUser.RefreshTokenId != null)
            {
                var refreshToken = _context.RefreshTokens.Single(
                    rt => rt.Id == adminUser.RefreshTokenId
                );
                _context.RefreshTokens.Remove(refreshToken);
                _context.SaveChanges();
            }

            _context.AdminUsers.Remove(adminUser);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertAdminUser(adminUser);
        }

        public AdminUserDto? Update(int adminUserId, UpdateAdminUserDto updateAdminUserDto)
        {
            var adminUser = _context.AdminUsers.SingleOrDefault(
                au => au.Id == adminUserId
            );

            if (adminUser == null)
            {
                return null;
            }

            adminUser.FirstName = updateAdminUserDto.FirstName;
            adminUser.LastName = updateAdminUserDto.LastName;
            adminUser.ImgLink = updateAdminUserDto.ImgLink;

            _context.AdminUsers.Update(adminUser);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertAdminUser(adminUser);
        }

        public void AddRefreshToken(int adminUserId, int TokenId)
        {
            var adminUser = _context.AdminUsers.Single(
                t => t.Id == adminUserId
            );

            if (adminUser.RefreshTokenId != null)
            {
                throw new Exception(
                    "Admin user already have a token, we cannot add another token"
                );
            }

            var refreshToken = _context.RefreshTokens.Single(
                t => t.Id == TokenId
            );

            adminUser.RefreshToken = refreshToken;

            _context.AdminUsers.Update(adminUser);
            _context.SaveChanges();
        }
    }
}
