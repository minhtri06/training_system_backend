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
        private readonly ILearningPathCertificateRepository _learningPathCertificateRepo;

        public TraineeController(
            ITraineeRepository traineeRepo,
            ITokenRepository tokenRepo,
            ILearningPathCertificateRepository learningPathCertificateRepo
        )
        {
            _traineeRepo = traineeRepo;
            _tokenRepo = tokenRepo;
            _learningPathCertificateRepo = learningPathCertificateRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllTrainees()
        {
            try
            {
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
        public IActionResult CreateTrainee(NewTraineeDto newTraineeDto)
        {
            try
            {
                if (_traineeRepo.CheckUsernameExist(newTraineeDto.Username))
                {
                    return BadRequest(
                        Utils.CommonResponse.USERNAME_ALREADY_EXISTS
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
            catch
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }
        }

        [HttpGet("{traineeId}")]
        [Authorize]
        public IActionResult GetTraineeById(int traineeId)
        {
            try
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
            catch
            {
                return StatusCode(
                    500,
                    Utils.CommonResponse.SOMETHING_WENT_WRONG
                );
            }
        }

        [HttpGet("{traineeId}/Certificates")]
        [Authorize]
        public IActionResult GetAllCertificatesOfATrainee(int traineeId)
        {
            try
            {
                if (_traineeRepo.CheckIdExist(traineeId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainee id")
                    );
                }

                var learningPaths =
                    _learningPathCertificateRepo.GetAllLearningPathsByTraineeId(
                        traineeId
                    );

                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "certificates of a trainee",
                        learningPaths
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

        [HttpPut("{traineeId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTraine(
            int traineeId,
            UpdateTraineeDto updateTraineeDto
        )
        {
            try
            {
                var updatedTrainee = _traineeRepo.Update(
                    traineeId,
                    updateTraineeDto
                );

                if (updatedTrainee == null)
                {
                    return BadRequest(
                        Utils.CommonResponse.UpdateObjectFailed("trainee")
                    );
                }

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "trainee",
                        updatedTrainee
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

        [HttpDelete("{traineeId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTrainee(int traineeId)
        {
            try
            {
                if (_traineeRepo.CheckIdExist(traineeId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainee")
                    );
                }

                var deletedTrainee = _traineeRepo.DeleteById(traineeId);

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "trainee",
                        deletedTrainee
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
                            trainee,
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
                        trainee,
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

                if (refreshToken.Token != refreshTokenRequestDto.RefreshToken)
                {
                    return BadRequest(Utils.CommonResponse.WRONG_REFRESH_TOKEN);
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
