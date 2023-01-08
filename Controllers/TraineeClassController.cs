using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using backend.Services.Interfaces;
using backend.Dto.TraineeClass;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeClassController : Controller
    {
        private readonly ITraineeClassRepository _traineeClassRepo;
        private readonly ITraineeRepository _traineeRepo;
        private readonly IClassRepository _classRepo;

        public TraineeClassController(
            ITraineeClassRepository traineeClassRepo,
            ITraineeRepository traineeRepo,
            IClassRepository classRepo
        )
        {
            _traineeClassRepo = traineeClassRepo;
            _traineeRepo = traineeRepo;
            _classRepo = classRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllTraineeClass()
        {
            try
            {
                var traineeClasses = _traineeClassRepo.GetAll();

                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "trainee classes",
                        traineeClasses
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
        public IActionResult CreateTraineeClass(
            NewTraineeClassDto newTraineeClassDto
        )
        {
            try
            {
                if (
                    _traineeRepo.CheckIdExist(newTraineeClassDto.TraineeId)
                    == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainee")
                    );
                }

                if (
                    _classRepo.CheckIdExist(newTraineeClassDto.ClassId) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("class")
                    );
                }

                if (
                    _traineeClassRepo.CheckIdExist(
                        newTraineeClassDto.TraineeId,
                        newTraineeClassDto.ClassId
                    )
                )
                {
                    return BadRequest(
                        Utils.CommonResponse.ObjectAlreadyExist("trainee class")
                    );
                }

                var newTraineeClass = _traineeClassRepo.Create(
                    newTraineeClassDto
                );

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "trainee class",
                        newTraineeClass
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

        [HttpGet("{traineeId}/{classId}")]
        [Authorize]
        public IActionResult GetTraineeClassById(int traineeId, int classId)
        {
            try
            {
                if (_traineeClassRepo.CheckIdExist(traineeId, classId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainee class")
                    );
                }

                var learningPathCourse = _traineeClassRepo.GetById(
                    traineeId,
                    classId
                );

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully(
                        "trainee class",
                        learningPathCourse
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

        [HttpDelete("{traineeId}/{classId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTraineeClassById(int traineeId, int classId)
        {
            try
            {
                if (_traineeClassRepo.CheckIdExist(traineeId, classId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainee class")
                    );
                }

                var deletedTraineeClass = _traineeClassRepo.DeleteById(
                    traineeId,
                    classId
                );

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "trainee class",
                        deletedTraineeClass
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

        [HttpPut("{traineeId}/{classId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTraineeClass(
            int traineeId,
            int classId,
            UpdateTraineeClassDto updateTraineeClassDto
        )
        {
            try
            {
                if (_traineeClassRepo.CheckIdExist(traineeId, classId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainee class")
                    );
                }

                var updatedTraineeClass = _traineeClassRepo.Update(
                    traineeId,
                    classId,
                    updateTraineeClassDto
                );

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "trainee class",
                        updatedTraineeClass
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
