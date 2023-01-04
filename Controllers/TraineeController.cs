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
        [Authorize]
        public IActionResult GetAllTrainees()
        {
            Console.WriteLine(SystemRole.Trainee.ToString());
            var trainees = _traineeRepo.GetAll();

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
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTrainee(NewTraineeDto newTraineeDto)
        {
            if (_traineeRepo.CheckUsernameExist(newTraineeDto.Username))
            {
                return BadRequest(Utils.CommonResponse.USERNAME_ALREADY_EXISTS);
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

            var newTrainee = _traineeRepo.Create(newTraineeDto);

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
        [Authorize]
        public IActionResult GetTraineeById(int traineeId)
        {
            var trainee = _traineeRepo.GetById(traineeId);

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
            try
            {
                var trainee = _traineeRepo.GetByLoginInfo(loginDto);

                if (trainee == null)
                {
                    return Unauthorized(
                        Utils.CommonResponse.WRONG_USERNAME_OR_PASSWORD
                    );
                }

                var newAccessTokenString = _tokenRepo.GenerateAccessTokenString(
                    trainee.Id,
                    trainee.FirstName,
                    trainee.Username,
                    trainee.SystemRole
                );
                var newRefreshTokenString =
                    _tokenRepo.GenerateRefreshTokenString();

                // If trainee does not have a token
                if (trainee.RefreshTokenId == null)
                {
                    var newRefreshToken = _tokenRepo.CreateRefreshToken(
                        newRefreshTokenString
                    );
                    _traineeRepo.AddRefreshToken(
                        trainee.Id,
                        newRefreshToken.Id
                    );

                    return Ok(
                        Utils.CommonResponse.LoginSuccessfully(
                            newAccessTokenString,
                            newRefreshTokenString
                        )
                    );
                }

                // If trainee already has a token
                _tokenRepo.RenewRefreshToken(
                    (int)trainee.RefreshTokenId,
                    newRefreshTokenString
                );

                return Ok(
                    Utils.CommonResponse.LoginSuccessfully(
                        newAccessTokenString,
                        newRefreshTokenString
                    )
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.ResponseException(ex.Message)
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
                var trainee = _traineeRepo.GetById(
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
                    Utils.CommonResponse.RefreshTokenSuccessfully(
                        newAccessToken
                    )
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.ResponseException(ex.Message)
                );
            }
        }
    }
}
