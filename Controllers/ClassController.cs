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
        public IActionResult GetAllClasses()
        {
            var classes = _classRepo.GetAll();
            return Ok(
                Utils.CommonResponse.GetAllObjectsSuccessfully(
                    "classes",
                    classes
                )
            );
        }

        [HttpGet("{classId}")]
        [Authorize]
        public IActionResult GetClassById(int classId)
        {
            var class_ = _classRepo.GetById(classId);

            if (class_ == null)
            {
                return NotFound(Utils.CommonResponse.ObjectNotFound("class"));
            }

            return Ok(
                Utils.CommonResponse.GetObjectSuccessfully("class", class_)
            );
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateClass(NewClassDto newClassDto)
        {
            var newClass = _classRepo.Create(newClassDto);

            return Ok(
                Utils.CommonResponse.CreateObjectSuccessfully("class", newClass)
            );
        }

        [HttpDelete("{classId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteClassById(int classId)
        {
            var class_ = _classRepo.DeleteById(classId);

            if (class_ == null)
            {
                return NotFound(Utils.CommonResponse.ObjectNotFound("class"));
            }

            return Ok(
                Utils.CommonResponse.DeleteObjectSuccessfully("class", class_)
            );
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
