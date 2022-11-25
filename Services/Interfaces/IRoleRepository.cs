using backend.Dto.Role;

namespace backend.Services.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<RoleDto> GetAll();
        RoleDto? GetById(int roleId);
        RoleDto CreateRole(NewRoleDto newRoleDto);
        int DeleteById(int roleId);
        int UpdateRole(int roleId, UpdateRoleDto updateRoleDto);
    }
}
