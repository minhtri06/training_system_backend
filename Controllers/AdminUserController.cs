using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.ApiResponse;
using backend.Dto.AdminUser;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : Controller
    {
        private readonly IAdminUserRepository _adminUserRepo;
        private readonly ITokenRepository _tokenRepo;

        public AdminUserController(
            IAdminUserRepository adminUserRepo,
            ITokenRepository tokenRepo
        )
        {
            _adminUserRepo = adminUserRepo;
            _tokenRepo = tokenRepo;
        }

        [HttpGet]
        // []
        public IActionResult GetAllAdminUsers()
        {
            var adminUsers = _adminUserRepo.GetAll();
            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Get all admin users successfully",
                    Data = adminUsers
                }
            );
        }

        [HttpPost]
        public IActionResult CreateAdminUser(NewAdminUserDto newAdminUserDto)
        {
            if (_adminUserRepo.CheckUsernameExist(newAdminUserDto.Username))
            {
                return BadRequest(Utils.CommonResponse.USERNAME_ALREADY_EXISTS);
            }

            var newAdminUser = _adminUserRepo.Create(newAdminUserDto);

            return Ok(
                Utils.CommonResponse.CreateSuccessfully(
                    "admin user",
                    newAdminUser
                )
            );
        }
    }
}
