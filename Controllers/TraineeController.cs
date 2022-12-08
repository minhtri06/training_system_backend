using Microsoft.AspNetCore.Mvc;

using backend.Dto.ApiResponse;
using backend.Dto.Trainee;
using backend.Services.Interfaces;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeController : Controller
    {
        private readonly ITraineeRepository _traineeRepo;

        public TraineeController(ITraineeRepository traineeRepo)
        {
            _traineeRepo = traineeRepo;
        }

        [HttpGet]
        public IActionResult GetAllTrainees()
        {
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
        public IActionResult GetTraineeById(int traineeId)
        {
            return Ok(_traineeRepo.GetTraineeById(traineeId));
        }
    }
}
