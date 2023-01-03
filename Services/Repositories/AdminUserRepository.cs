using backend.Dto.AdminUser;
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
                ? Utils.ConvertAdminUserToDto(adminUser)
                : null;
        }

        public IQueryable<AdminUserDto> GetAll()
        {
            var adminUserDtos =
                from adminUser in _context.AdminUsers
                select Utils.ConvertAdminUserToDto(adminUser);

            return adminUserDtos;
        }

        public AdminUserDto Create(NewAdminUserDto newAdminUserDto)
        {
            var salt = Utils.GenerateSalt(Utils.SALT_LENGTH);
            var passwordHash = Utils.HashPassword(
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

            return Utils.ConvertAdminUserToDto(newAdminUser);
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

            _context.AdminUsers.Remove(adminUser);
            _context.SaveChanges();

            return Utils.ConvertAdminUserToDto(adminUser);
        }

        public AdminUserDto? Update(UpdateAdminUserDto updateAdminUserDto)
        {
            var adminUser = _context.AdminUsers.SingleOrDefault(
                au => au.Id == updateAdminUserDto.Id
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

            return Utils.ConvertAdminUserToDto(adminUser);
        }
    }
}
