using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.DepartmentLearningPath;
using backend.Dto.ApiResponse;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentLearningPathController : Controller
    {
        private readonly IDepartmentLearningPathRepository _departmentLearningPathRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly ILearningPathRepository _learningPathRepo;

        public DepartmentLearningPathController(
            IDepartmentLearningPathRepository departmentLearningPathRepo,
            IDepartmentRepository departmentRepo,
            ILearningPathRepository learningPathRepo
        )
        {
            _departmentLearningPathRepo = departmentLearningPathRepo;
            _departmentRepo = departmentRepo;
            _learningPathRepo = learningPathRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllDepartmentLearningPathes()
        {
            try
            {
                var departmentLearningPaths =
                    _departmentLearningPathRepo.GetAll();
                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "departmentLearningPaths",
                        departmentLearningPaths
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

        [HttpGet("{departmentId}/{learningPathId}")]
        [Authorize]
        public IActionResult GetDepartmentLearningPathById(
            int departmentId,
            int learningPathId
        )
        {
            try
            {
                if (
                    _departmentLearningPathRepo.CheckIdExist(
                        departmentId,
                        learningPathId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound(
                            "departmentLearningPath"
                        )
                    );
                }

                var departmentLearningPath =
                    _departmentLearningPathRepo.GetById(
                        departmentId,
                        learningPathId
                    );

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully(
                        "departmentLearningPath",
                        departmentLearningPath
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
        public IActionResult CreateDepartmentLearningPath(
            NewDepartmentLearningPathDto newDepartmentLearningPathDto
        )
        {
            try
            {
                if (
                    _departmentRepo.CheckIdExist(
                        newDepartmentLearningPathDto.DepartmentId
                    ) == false
                )
                {
                    return BadRequest(
                        Utils.CommonResponse.ObjectNotFound("department")
                    );
                }

                if (
                    _learningPathRepo.CheckIdExist(
                        newDepartmentLearningPathDto.LearningPathId
                    ) == false
                )
                {
                    return BadRequest(
                        Utils.CommonResponse.ObjectNotFound("learningPath")
                    );
                }

                if (
                    _departmentLearningPathRepo.CheckIdExist(
                        newDepartmentLearningPathDto.DepartmentId,
                        newDepartmentLearningPathDto.LearningPathId
                    )
                )
                {
                    return BadRequest(
                        Utils.CommonResponse.ObjectAlreadyExist(
                            "departmennt learningpath"
                        )
                    );
                }

                var newDepartmentLearningPath =
                    _departmentLearningPathRepo.Create(
                        newDepartmentLearningPathDto
                    );

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "departmentLearningPath",
                        newDepartmentLearningPath
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

        [HttpDelete("{departmentId}/{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDepartmentLearningPathById(
            int departmentId,
            int learningPathId
        )
        {
            try
            {
                if (
                    _departmentLearningPathRepo.CheckIdExist(
                        departmentId,
                        learningPathId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound(
                            "departmentLearningPath"
                        )
                    );
                }

                var departmentLearningPath =
                    _departmentLearningPathRepo.DeleteById(
                        departmentId,
                        learningPathId
                    );

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "departmentLearningPath",
                        departmentLearningPath
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
