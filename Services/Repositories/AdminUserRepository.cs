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

        public AdminUserDto? GetAdminUserById(int adminUserId)
        {
            var adminUser = _context.AdminUsers.SingleOrDefault(
                au => au.Id == adminUserId
            );

            return adminUser != null
                ? Utils.ConvertAdminUserToDto(adminUser)
                : null;
        }
    }
}
