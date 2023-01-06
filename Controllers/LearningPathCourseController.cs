using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using backend.Services.Interfaces;
using backend.Dto.LearningPathCourse;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningPathCourseController : Controller
    {
        private readonly ILearningPathCourseRepository _learningPathCourseRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly ILearningPathRepository _learningPathRepo;

        public LearningPathCourseController(
            ILearningPathCourseRepository learningPathCourseRepo,
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
        public IActionResult GetAllLearningPathCourse()
        {
            var learningPathCourses = _learningPathCourseRepo.GetAll();

            return Ok(
                Utils.CommonResponse.GetAllObjectsSuccessfully(
                    "learning path courses",
                    learningPathCourses
                )
            );
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateLearningPathCourse(
            NewLearningPathCourseDto newLearningPathCourseDto
        )
        {
            try
            {
                if (
                    _courseRepo.CheckIdExist(
                        newLearningPathCourseDto.CourseId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("course")
                    );
                }

                if (
                    _learningPathRepo.CheckIdExist(
                        newLearningPathCourseDto.LearningPathId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("learningPath")
                    );
                }

                if (
                    _learningPathCourseRepo.CheckIdExist(
                        newLearningPathCourseDto.CourseId,
                        newLearningPathCourseDto.LearningPathId
                    )
                )
                {
                    return BadRequest(
                        Utils.CommonResponse.ObjectAlreadyExist(
                            "learningpath course"
                        )
                    );
                }

                var newLearningPathCourse =
                    _learningPathCourseRepo.Create(
                        newLearningPathCourseDto
                    );

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "learningpath course",
                        newLearningPathCourse
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
        public IActionResult GetLearningPathCourseById(int courseId, int learningPathId)
        {
            if (_learningPathCourseRepo.CheckIdExist(courseId, learningPathId) == false)
            {
                return NotFound(
                    Utils.CommonResponse.ObjectNotFound("learning path course")
                );
            }

            var learningPathCourse = _learningPathCourseRepo.GetById(courseId, learningPathId);

            return Ok(
                Utils.CommonResponse.GetObjectSuccessfully(
                    "learning path course",
                    learningPathCourse
                )
            );
        }

        [HttpDelete("{courseId}/{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteLearningPathCourseById(int courseId, int learningPathId)
        {
            if (_learningPathCourseRepo.CheckIdExist(courseId, learningPathId) == false)
            {
                return NotFound(
                    Utils.CommonResponse.ObjectNotFound("learning path course")
                );
            }

            var deletedLearningPathCourse = _learningPathCourseRepo.DeleteById(
                courseId, learningPathId
            );

            return Ok(
                Utils.CommonResponse.DeleteObjectSuccessfully(
                    "learning path course",
                    deletedLearningPathCourse
                )
            );
        }

        [HttpPut("{courseId}/{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateLearningPathCourse(
            int courseId, int learningPathId,
            UpdateLearningPathCourseDto updateLearningPathCourseDto
        )
        {
            if (_learningPathCourseRepo.CheckIdExist(courseId, learningPathId) == false)
            {
                return NotFound(
                    Utils.CommonResponse.ObjectNotFound("learning path course")
                );
            }

            var updatedLearningPathCourse = _learningPathCourseRepo.Update(
                courseId, learningPathId,
                updateLearningPathCourseDto
            );

            return Ok(
                Utils.CommonResponse.UpdateObjectSuccessfully(
                    "learning path course",
                    updatedLearningPathCourse
                )
            );
        }
    }
}
