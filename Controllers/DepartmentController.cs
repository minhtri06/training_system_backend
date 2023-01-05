using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.Department;
using backend.Dto.ApiResponse;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllDepartmentes()
        {
            try
            {
                var departments = _departmentRepo.GetAll();
                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "departments",
                        departments
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

        [HttpGet("{departmentId}")]
        [Authorize]
        public IActionResult GetDepartmentById(int departmentId)
        {
            try
            {
                if (_departmentRepo.CheckIdExist(departmentId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("department")
                    );
                }

                var department = _departmentRepo.GetById(departmentId);

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully(
                        "department",
                        department
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
        public IActionResult CreateDepartment(NewDepartmentDto newDepartmentDto)
        {
            try
            {
                var newDepartment = _departmentRepo.Create(newDepartmentDto);

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "department",
                        newDepartment
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

        [HttpDelete("{departmentId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteDepartmentById(int departmentId)
        {
            try
            {
                if (_departmentRepo.CheckIdExist(departmentId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("department")
                    );
                }
                var department = _departmentRepo.DeleteById(departmentId);

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "department",
                        department
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

        [HttpPut("{departmentId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateDepartment(
            int departmentId,
            UpdateDepartmentDto updateDepartmentDto
        )
        {
            try
            {
                if (_departmentRepo.CheckIdExist(departmentId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("department")
                    );
                }
                var department = _departmentRepo.Update(
                    departmentId,
                    updateDepartmentDto
                );

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "department",
                        department
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
