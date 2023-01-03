using backend.Dto.Role;

namespace backend.Services.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<RoleDto> GetAll();
        RoleDto? GetById(int roleId);
        RoleDto Create(NewRoleDto newRoleDto);
        int DeleteById(int roleId);
        int Update(int roleId, UpdateRoleDto updateRoleDto);
    }
}
