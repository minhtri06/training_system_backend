using backend.Dto.AdminUser;
using backend.Dto.Login;

namespace backend.Services.Interfaces
{
    public interface IAdminUserRepository
    {
        bool CheckUsernameExist(string username);
        AdminUserDto? GetById(int adminUserId);
        AdminUserDto? GetByLoginInfo(LoginDto loginDto);
        ICollection<AdminUserDto> GetAll();
        AdminUserDto Create(NewAdminUserDto newAdminUserDto);
        AdminUserDto? DeleteById(int adminUserId);
        AdminUserDto? Update(
            int adminUserId,
            UpdateAdminUserDto updateAdminUserDto
        );
        void AddRefreshToken(int traineeId, int TokenId);
    }
}
