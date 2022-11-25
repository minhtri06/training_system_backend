using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.Role;
using backend.Dto.ApiResponse;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepo;

        public RoleController(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleRepo.GetAll();
            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Get all roles successfully",
                    Data = roles
                }
            );
        }

        [HttpGet("{roleId}")]
        public IActionResult GetRoleById(int roleId)
        {
            var role = _roleRepo.GetById(roleId);

            if (role == null)
            {
                return NotFound(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Role not found, get role failed"
                    }
                );
            }

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Get role successfully",
                    Data = new List<RoleDto>() { role }
                }
            );
        }

        [HttpPost]
        public IActionResult CreateRole(NewRoleDto newRoleDto)
        {
            var newRole = _roleRepo.CreateRole(newRoleDto);

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Create role successfully",
                    Data = new List<RoleDto>() { newRole }
                }
            );
        }

        [HttpDelete("{roleId}")]
        public IActionResult DeleteRoleById(int roleId)
        {
            var result = _roleRepo.DeleteById(roleId);

            if (result == -1)
            {
                return NotFound(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Role not found, delete role failed",
                    }
                );
            }

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Delete role successfully",
                    Data = new { roleId }
                }
            );
        }

        [HttpPut("{roleId}")]
        public IActionResult UpdateRole(int roleId, UpdateRoleDto updateRoleDto)
        {
            if (_roleRepo.UpdateRole(roleId, updateRoleDto) == -1)
            {
                return NotFound(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Role not found, update role failed"
                    }
                );
            }

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Update successfully",
                }
            );
        }
    }
}
