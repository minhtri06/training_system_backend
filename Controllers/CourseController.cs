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
        public IActionResult GetAllCourses()
        {
            var courses = _courseRepo.GetAll();
            return Ok(
                Utils.CommonResponse.GetAllObjectsSuccessfully("courses", courses)
            );
        }

        [HttpGet("{courseId}")]
        [Authorize]
        public IActionResult GetCourseById(int courseId)
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCourse(NewCourseDto newCourseDto)
        {
            var newCourse = _courseRepo.Create(newCourseDto);

            return Ok(
                Utils.CommonResponse.CreateObjectSuccessfully("course", newCourse)
            );
        }

        [HttpDelete("{courseId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCourseById(int courseId)
        {
            var course = _courseRepo.DeleteById(courseId);

            if (course == null)
            {
                return NotFound(
                    Utils.CommonResponse.ObjectNotFound("course")
                );
            }

            return Ok(
                Utils.CommonResponse.DeleteObjectSuccessfully("course", course)
            );
        }

        [HttpPut("{courseId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCourse(int courseId, UpdateCourseDto updateCourseDto)
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
                    Utils.CommonResponse.UpdateObjectSuccessfully("course", course)
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
