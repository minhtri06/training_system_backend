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
        private readonly ITraineeClassRepository _learningPathCourseRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly ILearningPathRepository _learningPathRepo;

        public TraineeClassController(
            ITraineeClassRepository learningPathCourseRepo,
            ICourseRepository courseRepo,
            ILearningPathRepository learningPathRepo
        )
        {
            _learningPathCourseRepo = learningPathCourseRepo;
            _courseRepo = courseRepo;
            _learningPathRepo = learningPathRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllTraineeClass()
        {
            try
            {
                var learningPathCourses = _learningPathCourseRepo.GetAll();

                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "learning path courses",
                        learningPathCourses
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
                    _courseRepo.CheckIdExist(newTraineeClassDto.CourseId)
                    == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("course")
                    );
                }

                if (
                    _learningPathRepo.CheckIdExist(
                        newTraineeClassDto.LearningPathId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("learningPath")
                    );
                }

                if (
                    _learningPathCourseRepo.CheckIdExist(
                        newTraineeClassDto.CourseId,
                        newTraineeClassDto.LearningPathId
                    )
                )
                {
                    return BadRequest(
                        Utils.CommonResponse.ObjectAlreadyExist(
                            "learningpath course"
                        )
                    );
                }

                var newTraineeClass = _learningPathCourseRepo.Create(
                    newTraineeClassDto
                );

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "learningpath course",
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

        [HttpGet("{courseId}/{learningPathId}")]
        [Authorize]
        public IActionResult GetTraineeClassById(
            int courseId,
            int learningPathId
        )
        {
            try
            {
                if (
                    _learningPathCourseRepo.CheckIdExist(
                        courseId,
                        learningPathId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound(
                            "learning path course"
                        )
                    );
                }

                var learningPathCourse = _learningPathCourseRepo.GetById(
                    courseId,
                    learningPathId
                );

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully(
                        "learning path course",
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

        [HttpDelete("{courseId}/{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTraineeClassById(
            int courseId,
            int learningPathId
        )
        {
            try
            {
                if (
                    _learningPathCourseRepo.CheckIdExist(
                        courseId,
                        learningPathId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound(
                            "learning path course"
                        )
                    );
                }

                var deletedTraineeClass =
                    _learningPathCourseRepo.DeleteById(
                        courseId,
                        learningPathId
                    );

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "learning path course",
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

        [HttpPut("{courseId}/{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTraineeClass(
            int courseId,
            int learningPathId,
            UpdateTraineeClassDto updateTraineeClassDto
        )
        {
            try
            {
                if (
                    _learningPathCourseRepo.CheckIdExist(
                        courseId,
                        learningPathId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound(
                            "learning path course"
                        )
                    );
                }

                var updatedTraineeClass = _learningPathCourseRepo.Update(
                    courseId,
                    learningPathId,
                    updateTraineeClassDto
                );

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "learning path course",
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
