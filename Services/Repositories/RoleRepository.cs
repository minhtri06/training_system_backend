using backend.Models;
using backend.Dto.Role;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckIdExist(int roleId)
        {
            return _context.Roles.Any(r => r.Id == roleId);
        }

        public ICollection<RoleDto> GetAll()
        {
            return _context.Roles
                .Select(r => new RoleDto() { Id = r.Id, Name = r.Name })
                .ToList();
        }

        public RoleDto? GetById(int roleId)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Id == roleId);

            if (role != null)
                return new RoleDto() { Id = role.Id, Name = role.Name };
            return null;
        }

        public RoleDto Create(NewRoleDto newRoleDto)
        {
            var newRole = new Role() { Name = newRoleDto.Name };

            _context.Roles.Add(newRole);
            _context.SaveChanges();

            return new RoleDto() { Id = newRole.Id, Name = newRole.Name };
        }

        public int DeleteById(int roleId)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Id == roleId);

            if (role == null)
                return -1;

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return roleId;
        }

        public int Update(int roleId, UpdateRoleDto updateRoleDto)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Id == roleId);

            if (role == null)
            {
                return -1;
            }

            role.Name = updateRoleDto.Name;

            _context.Roles.Update(role);
            _context.SaveChanges();

            return roleId;
        }
    }
}
