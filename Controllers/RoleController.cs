using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.Role;
using backend.Dto.ApiResponse;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
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
        [Authorize]
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
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole(NewRoleDto newRoleDto)
        {
            var newRole = _roleRepo.Create(newRoleDto);

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateRole(int roleId, UpdateRoleDto updateRoleDto)
        {
            if (_roleRepo.Update(roleId, updateRoleDto) == -1)
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
