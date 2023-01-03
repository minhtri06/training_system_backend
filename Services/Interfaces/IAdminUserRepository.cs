using backend.Dto.AdminUser;

namespace backend.Services.Interfaces
{
    public interface IAdminUserRepository
    {
        bool CheckUsernameExist(string username);
        AdminUserDto? GetById(int adminUserId);
        IQueryable<AdminUserDto> GetAll();
        AdminUserDto Create(NewAdminUserDto newAdminUserDto);
        AdminUserDto? DeleteById(int adminUserId);
        AdminUserDto? Update(UpdateAdminUserDto updateAdminUserDto);
    }
}
