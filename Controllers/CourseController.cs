using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.Course;
using backend.Dto.ApiResponse;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepo;

        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllCourses(
            [FromQuery(Name = "learning-path-id")] int? learningPathId
        )
        {
            try
            {
                if (learningPathId != null)
                {
                    return Ok(
                        Utils.CommonResponse.GetAllObjectsSuccessfully(
                            "courses of learning path id "
                                + learningPathId.ToString(),
                            _courseRepo.GetAllByLearningPathId(
                                (int)learningPathId
                            )
                        )
                    );
                }
                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "courses",
                        _courseRepo.GetAll()
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

        [HttpGet("{courseId}")]
        [Authorize]
        public IActionResult GetCourseById(int courseId)
        {
            try
            {
                var course = _courseRepo.GetById(courseId);

                if (course == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("course")
                    );
                }

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully("course", course)
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
        public IActionResult CreateCourse(NewCourseDto newCourseDto)
        {
            try
            {
                var newCourse = _courseRepo.Create(newCourseDto);

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "course",
                        newCourse
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

        [HttpDelete("{courseId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCourseById(int courseId)
        {
            try
            {
                var course = _courseRepo.DeleteById(courseId);

                if (course == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("course")
                    );
                }

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "course",
                        course
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

        [HttpPut("{courseId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCourse(
            int courseId,
            UpdateCourseDto updateCourseDto
        )
        {
            try
            {
                var course = _courseRepo.Update(courseId, updateCourseDto);

                if (course == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("course")
                    );
                }

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "course",
                        course
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
