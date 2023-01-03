using backend.Dto.AdminUser;

namespace backend.Services.Interfaces
{
    public interface IAdminUserRepository
    {
        AdminUserDto? GetAdminUserById(int adminUserId);
    }
}
