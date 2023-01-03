using Microsoft.AspNetCore.Mvc;

using backend.Dto.ApiResponse;
using backend.Dto.Trainee;
using backend.Dto.Login;
using backend.Dto.Token;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeController : Controller
    {
        private readonly ITraineeRepository _traineeRepo;
        private readonly ITokenRepository _tokenRepo;

        public TraineeController(
            ITraineeRepository traineeRepo,
            ITokenRepository tokenRepo
        )
        {
            _traineeRepo = traineeRepo;
            _tokenRepo = tokenRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Trainer, Trainee")]
        public IActionResult GetAllTrainees()
        {
            Console.WriteLine(SystemRole.Trainee.ToString());
            var trainees = _traineeRepo.GetAllTrainees();

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Get all trainees successfull",
                    Data = trainees
                }
            );
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public IActionResult CreateTrainee(NewTraineeDto newTraineeDto)
        {
            if (_traineeRepo.CheckUsernameExist(newTraineeDto.Username))
            {
                return BadRequest(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Username already exists"
                    }
                );
            }

            if (newTraineeDto.Username.Length < 3)
            {
                return BadRequest(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Username must longer than 2 characters"
                    }
                );
            }

            if (newTraineeDto.Password.Length < 3)
            {
                return BadRequest(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Password must longer than 2 characters"
                    }
                );
            }

            var newTrainee = _traineeRepo.CreateTrainee(newTraineeDto);

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Trainee created successfully",
                    Data = newTrainee
                }
            );
        }

        [HttpGet("{traineeId}")]
        [Authorize(Roles = "0, 1, 2")]
        public IActionResult GetTraineeById(int traineeId)
        {
            var trainee = _traineeRepo.GetTraineeById(traineeId);

            if (trainee == null)
            {
                return NotFound(Utils.CommonResponse.NOT_FOUND);
            }

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Get trainee successfully",
                    Data = new List<TraineeDto>() { trainee }
                }
            );
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var trainee = _traineeRepo.GetTraineeByLoginInfo(loginDto);

            if (trainee == null)
            {
                return Unauthorized(
                    Utils.CommonResponse.WRONG_USERNAME_OR_PASSWORD
                );
            }

            var newAccessToken = _tokenRepo.GenerateAccessTokenString(
                trainee.Id,
                trainee.FirstName,
                trainee.Username,
                trainee.SystemRole
            );

            var newRefreshToken = _tokenRepo.GenerateRefreshTokenString();

            // If trainee does not have a token
            if (trainee.RefreshTokenId == null)
            {
                var newToken = _tokenRepo.CreateRefreshToken(newRefreshToken);

                if (_traineeRepo.AddRefreshToken(trainee.Id, newToken.Id) != 0)
                {
                    return StatusCode(
                        500,
                        Utils.CommonResponse.SOMETHING_WENT_WRONG
                    );
                }

                return Ok(
                    Utils.CommonResponse.LoginSuccessfully(
                        newAccessToken,
                        newRefreshToken
                    )
                );
            }

            // If trainee already have a token
            if (
                _tokenRepo.RenewRefreshToken(
                    (int)trainee.RefreshTokenId,
                    newRefreshToken
                ) != 0
            )
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }

            return Ok(
                Utils.CommonResponse.LoginSuccessfully(
                    newAccessToken,
                    newRefreshToken
                )
            );
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(
            RefreshTokenRequestDto refreshTokenRequestDto
        )
        {
            var trainee = _traineeRepo.GetTraineeById(
                refreshTokenRequestDto.UserId
            );

            if (trainee == null)
            {
                return NotFound(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Trainee not found",
                    }
                );
            }

            if (trainee.RefreshTokenId == null)
            {
                return BadRequest(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "You doesn't have a token"
                    }
                );
            }

            var refreshToken = _tokenRepo.GetRefreshTokenById(
                (int)trainee.RefreshTokenId
            );
            if (refreshToken == null)
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }

            if (refreshToken.ExpiryTime < DateTime.UtcNow)
            {
                return StatusCode(403, Utils.CommonResponse.FORBIDDEN);
            }

            var newAccessToken = _tokenRepo.GenerateAccessTokenString(
                trainee.Id,
                trainee.FirstName,
                trainee.Username,
                trainee.SystemRole
            );

            return Ok(
                Utils.CommonResponse.RefreshTokenSuccessfully(newAccessToken)
            );
        }
    }
}
