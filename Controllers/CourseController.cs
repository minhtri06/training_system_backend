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
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Get all courses successfully",
                    Data = courses
                }
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
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Course not found, get course failed"
                    }
                );
            }

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Get course successfully",
                    Data = new List<CourseDto>() { course }
                }
            );
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCourse(NewCourseDto newCourseDto)
        {
            var newCourse = _courseRepo.Create(newCourseDto);

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Create course successfully",
                    Data = new List<CourseDto>() { newCourse }
                }
            );
        }

        [HttpDelete("{courseId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCourseById(int courseId)
        {
            var result = _courseRepo.DeleteById(courseId);

            if (result == -1)
            {
                return NotFound(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Course not found, delete course failed",
                    }
                );
            }

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Delete course successfully",
                    Data = new { courseId }
                }
            );
        }

        [HttpPut("{courseId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCourse(int courseId, UpdateCourseDto updateCourseDto)
        {
            if (_courseRepo.Update(courseId, updateCourseDto) == -1)
            {
                return NotFound(
                    new ApiResponseDto()
                    {
                        Success = false,
                        Message = "Course not found, update course failed"
                    }
                );
            }

            return Ok(
                new ApiResponseDto()
                {
                    Success = true,
                    Message = "Update successfully",
                }
            );
        }
    }
}
