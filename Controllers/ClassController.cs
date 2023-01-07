using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.Class;
using backend.Dto.ApiResponse;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IClassRepository _classRepo;

        public ClassController(IClassRepository classRepo)
        {
            _classRepo = classRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllClasses(
            [FromQuery(Name = "courseId")] int? courseId
        )
        {
            try
            {
                ICollection<ClassDto> classes;

                if (courseId != null)
                {
                    classes = _classRepo.GetAllByCourseId((int)courseId);
                }
                else
                {
                    classes = _classRepo.GetAll();
                }
                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "classes",
                        classes
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

        [HttpGet("{classId}")]
        [Authorize]
        public IActionResult GetClassById(int classId)
        {
            try
            {
                var class_ = _classRepo.GetById(classId);

                if (class_ == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("class")
                    );
                }

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully("class", class_)
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateClass(NewClassDto newClassDto)
        {
            try
            {
                var newClass = _classRepo.Create(newClassDto);

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "class",
                        newClass
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

        [HttpDelete("{classId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteClassById(int classId)
        {
            try
            {
                var class_ = _classRepo.DeleteById(classId);

                if (class_ == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("class")
                    );
                }

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "class",
                        class_
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

        [HttpPut("{classId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateClass(
            int classId,
            UpdateClassDto updateClassDto
        )
        {
            try
            {
                var class_ = _classRepo.Update(classId, updateClassDto);

                if (class_ == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("class")
                    );
                }

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "class",
                        class_
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
