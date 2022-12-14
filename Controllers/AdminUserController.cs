using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.ApiResponse;
using backend.Dto.AdminUser;
using backend.Dto.Login;
using Microsoft.AspNetCore.Authorization;
using backend.Dto.Token;

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
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllAdminUsers()
        {
            try
            {
                var adminUsers = _adminUserRepo.GetAll();

                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "admin user",
                        adminUsers
                    )
                );
            }
            catch
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAdminUser(NewAdminUserDto newAdminUserDto)
        {
            try
            {
                if (_adminUserRepo.CheckUsernameExist(newAdminUserDto.Username))
                {
                    return BadRequest(
                        Utils.CommonResponse.USERNAME_ALREADY_EXISTS
                    );
                }

                var newAdminUser = _adminUserRepo.Create(newAdminUserDto);

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "admin user",
                        newAdminUser
                    )
                );
            }
            catch
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }
        }

        [HttpGet("{adminUserId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminUserById(int adminUserId)
        {
            try
            {
                var adminUser = _adminUserRepo.GetById(adminUserId);

                if (adminUser == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("admin user")
                    );
                }

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully(
                        "admin user",
                        adminUser
                    )
                );
            }
            catch
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }
        }

        [HttpDelete("{adminUserId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAdminUserById(int adminUserId)
        {
            try
            {
                var adminUser = _adminUserRepo.DeleteById(adminUserId);

                if (adminUser == null)
                {
                    return BadRequest(
                        new ApiResponseDto()
                        {
                            Success = false,
                            Message = "Delete fail"
                        }
                    );
                }

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "admin user",
                        adminUser
                    )
                );
            }
            catch
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }
        }

        [HttpPut("{adminUserId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateAdminUser(
            int adminUserId,
            UpdateAdminUserDto updateAdminUserDto
        )
        {
            try
            {
                var adminUser = _adminUserRepo.Update(
                    adminUserId,
                    updateAdminUserDto
                );

                if (adminUser == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("adminUser")
                    );
                }

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "adminUser",
                        adminUser
                    )
                );
            }
            catch
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }
        }

        // Each time the user login, the fresh token of that user will
        // be created or renewed
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {
                var adminUser = _adminUserRepo.GetByLoginInfo(loginDto);
                // Login fail
                if (adminUser == null)
                {
                    return Unauthorized(
                        Utils.CommonResponse.WRONG_USERNAME_OR_PASSWORD
                    );
                }

                // Login success
                var newAccessTokenString = _tokenRepo.GenerateAccessTokenString(
                    adminUser.Id,
                    adminUser.FirstName,
                    adminUser.Username,
                    adminUser.SystemRole
                );
                var newRefreshTokenString =
                    _tokenRepo.GenerateRefreshTokenString();

                // If admin user does not have a token
                if (adminUser.RefreshTokenId == null)
                {
                    var newRefreshToken = _tokenRepo.CreateRefreshToken(
                        newRefreshTokenString
                    );
                    _adminUserRepo.AddRefreshToken(
                        adminUser.Id,
                        newRefreshToken.Id
                    );

                    return Ok(
                        Utils.CommonResponse.LoginSuccessfully(
                            adminUser,
                            newAccessTokenString,
                            newRefreshTokenString
                        )
                    );
                }

                // If admin user already has a token
                _tokenRepo.RenewRefreshToken(
                    (int)adminUser.RefreshTokenId,
                    newRefreshTokenString
                );

                return Ok(
                    Utils.CommonResponse.LoginSuccessfully(
                        adminUser,
                        newAccessTokenString,
                        newRefreshTokenString
                    )
                );
            }
            catch
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(
            RefreshTokenRequestDto refreshTokenRequestDto
        )
        {
            try
            {
                var adminUser = _adminUserRepo.GetById(
                    refreshTokenRequestDto.UserId
                );

                if (adminUser == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("admin user")
                    );
                }

                if (adminUser.RefreshTokenId == null)
                {
                    return BadRequest(
                        new ApiResponseDto()
                        {
                            Success = false,
                            Message = "you doesn't have a token"
                        }
                    );
                }

                var refreshToken = _tokenRepo.GetRefreshTokenById(
                    (int)adminUser.RefreshTokenId
                );
                if (refreshToken == null)
                {
                    return StatusCode(
                        500,
                        Utils.CommonResponse.SOMETHING_WENT_WRONG
                    );
                }

                if (refreshToken.Token != refreshTokenRequestDto.RefreshToken)
                {
                    return BadRequest(Utils.CommonResponse.WRONG_REFRESH_TOKEN);
                }

                // If refresh token is expired
                if (refreshToken.ExpiryTime < DateTime.UtcNow)
                {
                    return StatusCode(403, Utils.CommonResponse.FORBIDDEN);
                }

                var newAccessToken = _tokenRepo.GenerateAccessTokenString(
                    adminUser.Id,
                    adminUser.FirstName,
                    adminUser.Username,
                    adminUser.SystemRole
                );

                return Ok(
                    Utils.CommonResponse.RefreshTokenSuccessfully(
                        newAccessToken
                    )
                );
            }
            catch
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }
        }
    }
}
