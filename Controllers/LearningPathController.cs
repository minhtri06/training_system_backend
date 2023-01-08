using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using backend.Services.Interfaces;
using backend.Dto.LearningPath;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningPathController : Controller
    {
        private readonly ILearningPathRepository _learningPathRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly ILearningPathCertificateRepository _learningPathCertificateRepo;
        private readonly IDepartmentLearningPathRepository _departmentLearningPathRepo;

        public LearningPathController(
            ILearningPathRepository learningPathRepo,
            IRoleRepository roleRepo,
            ILearningPathCertificateRepository learningPathCertificateRepo,
            IDepartmentLearningPathRepository departmentLearningPathRepo
        )
        {
            _learningPathRepo = learningPathRepo;
            _roleRepo = roleRepo;
            _learningPathCertificateRepo = learningPathCertificateRepo;
            _departmentLearningPathRepo = departmentLearningPathRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllLearningPath()
        {
            try
            {
                var learningPaths = _learningPathRepo.GetAll();

                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "learning paths",
                        learningPaths
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
        public IActionResult CreateLearningPath(
            NewLearningPathDto newLearningPathDto
        )
        {
            try
            {
                if (newLearningPathDto.ForRoleId != null)
                {
                    if (
                        _roleRepo.CheckIdExist(
                            (int)newLearningPathDto.ForRoleId
                        ) == false
                    )
                    {
                        return NotFound(
                            Utils.CommonResponse.ObjectNotFound("role id")
                        );
                    }
                }

                var learningPath = _learningPathRepo.Create(newLearningPathDto);

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "learning path",
                        learningPath
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

        [HttpGet("{learningPathId}")]
        [Authorize]
        public IActionResult GetLearningPathById(int learningPathId)
        {
            try
            {
                if (_learningPathRepo.CheckIdExist(learningPathId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("learning path")
                    );
                }

                var learningPath = _learningPathRepo.GetById(learningPathId);

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully(
                        "learning path",
                        learningPath
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

        [HttpGet("{learningPathId}/CertificatedTrainees")]
        [Authorize]
        public IActionResult GetCertificatedTraineesOfALearningPath(
            int learningPathId
        )
        {
            try
            {
                if (_learningPathRepo.CheckIdExist(learningPathId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("learning path")
                    );
                }

                var certificatedTrainees =
                    _learningPathCertificateRepo.GetAllCertificatedTraineesByLearningPathId(
                        learningPathId
                    );

                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "certificated trainees",
                        certificatedTrainees
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

        [HttpGet("{learningPathId}/Departments")]
        [Authorize]
        public IActionResult GetDepartmentsOfALearningPath(int learningPathId)
        {
            try
            {
                if (_learningPathRepo.CheckIdExist(learningPathId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("learning path")
                    );
                }

                var departments =
                    _departmentLearningPathRepo.GetAllDepartmentsOfALearningPath(
                        learningPathId
                    );

                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "certificated trainees",
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

        [HttpDelete("{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteLearningPathById(int learningPathId)
        {
            try
            {
                if (_learningPathRepo.CheckIdExist(learningPathId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("learning path")
                    );
                }

                var deletedLearningPath = _learningPathRepo.DeleteById(
                    learningPathId
                );

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "learning path",
                        deletedLearningPath
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

        [HttpPut("{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateLearningPath(
            int learningPathId,
            UpdateLearningPathDto updateLearningPathDto
        )
        {
            try
            {
                if (_learningPathRepo.CheckIdExist(learningPathId) == false)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("learning path id")
                    );
                }

                if (updateLearningPathDto.ForRoleId != null)
                {
                    if (
                        _roleRepo.CheckIdExist(
                            (int)updateLearningPathDto.ForRoleId
                        ) == false
                    )
                    {
                        return NotFound(
                            Utils.CommonResponse.ObjectNotFound("role id")
                        );
                    }
                }

                var updatedLearningPath = _learningPathRepo.Update(
                    learningPathId,
                    updateLearningPathDto
                );

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "learning path",
                        updatedLearningPath
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
