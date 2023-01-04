using backend.Dto.AdminUser;
using backend.Dto.Login;

namespace backend.Services.Interfaces
{
    public interface IAdminUserRepository
    {
        bool CheckUsernameExist(string username);
        AdminUserDto? GetById(int adminUserId);
        AdminUserDto? GetByLoginInfo(LoginDto loginDto);
        IQueryable<AdminUserDto> GetAll();
        AdminUserDto Create(NewAdminUserDto newAdminUserDto);
        AdminUserDto? DeleteById(int adminUserId);
        AdminUserDto? Update(UpdateAdminUserDto updateAdminUserDto);
        void AddRefreshToken(int traineeId, int TokenId);
    }
}
