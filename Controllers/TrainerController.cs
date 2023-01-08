using Microsoft.AspNetCore.Mvc;

using backend.Dto.ApiResponse;
using backend.Dto.Login;
using backend.Dto.Token;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using backend.Dto.Trainer;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : Controller
    {
        private readonly ITrainerRepository _trainerRepo;

        public TrainerController(ITrainerRepository trainerRepo)
        {
            _trainerRepo = trainerRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllTrainers()
        {
            try
            {
                var trainers = _trainerRepo.GetAll();

                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "trainers",
                        trainers
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
        public IActionResult CreateTrainer(NewTrainerDto newTrainerDto)
        {
            try
            {
                if (_trainerRepo.CheckUsernameExist(newTrainerDto.Username))
                {
                    return BadRequest(
                        Utils.CommonResponse.USERNAME_ALREADY_EXISTS
                    );
                }

                if (newTrainerDto.Username.Length < 3)
                {
                    return BadRequest(
                        new ApiResponseDto()
                        {
                            Success = false,
                            Message = "Username must longer than 2 characters"
                        }
                    );
                }

                if (newTrainerDto.Password.Length < 3)
                {
                    return BadRequest(
                        new ApiResponseDto()
                        {
                            Success = false,
                            Message = "Password must longer than 2 characters"
                        }
                    );
                }

                var newTrainer = _trainerRepo.Create(newTrainerDto);

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "trainer",
                        newTrainer
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

        [HttpGet("{trainerId}")]
        [Authorize]
        public IActionResult GetTrainerById(int trainerId)
        {
            try
            {
                if (_trainerRepo.CheckIdExist(trainerId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainer")
                    );
                }

                var trainer = _trainerRepo.GetById(trainerId);

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully(
                        "trainer",
                        trainer
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

        [HttpPut("{trainerId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTrainer(
            int trainerId,
            UpdateTrainerDto updateTrainerDto
        )
        {
            try
            {
                if (_trainerRepo.CheckIdExist(trainerId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainer")
                    );
                }

                var updatedTrainer = _trainerRepo.Update(
                    trainerId,
                    updateTrainerDto
                );

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "trainer",
                        updatedTrainer
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

        [HttpDelete("{trainerId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTrainerById(int trainerId)
        {
            try
            {
                if (_trainerRepo.CheckIdExist(trainerId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainer")
                    );
                }

                var deletedTrainer = _trainerRepo.DeleteById(trainerId);

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "trainer",
                        deletedTrainer
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
