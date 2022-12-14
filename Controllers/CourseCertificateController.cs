using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.CourseCertificate;
using backend.Dto.ApiResponse;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCertificateController : Controller
    {
        private readonly ICourseCertificateRepository _courseCertificateRepo;

        public CourseCertificateController(
            ICourseCertificateRepository courseCertificateRepo
        )
        {
            _courseCertificateRepo = courseCertificateRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllCourseCertificates()
        {
            try
            {
                var courseCertificates = _courseCertificateRepo.GetAll();
                return Ok(
                    Utils.CommonResponse.GetAllObjectsSuccessfully(
                        "courseCertificates",
                        courseCertificates
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

        [HttpGet("{traineeId}/{courseId}")]
        [Authorize]
        public IActionResult GetCourseCertificateById(
            int traineeId,
            int courseId
        )
        {
            try
            {
                var courseCertificate = _courseCertificateRepo.GetById(
                    traineeId,
                    courseId
                );

                if (courseCertificate == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("courseCertificate")
                    );
                }

                return Ok(
                    Utils.CommonResponse.GetObjectSuccessfully(
                        "courseCertificate",
                        courseCertificate
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
        public IActionResult CreateCourseCertificate(
            NewCourseCertificateDto newCourseCertificateDto
        )
        {
            try
            {
                var newCourseCertificate = _courseCertificateRepo.Create(
                    newCourseCertificateDto
                );

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "courseCertificate",
                        newCourseCertificate
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

        [HttpDelete("{traineeId}/{courseId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCourseCertificateById(
            int traineeId,
            int courseId
        )
        {
            try
            {
                var courseCertificate = _courseCertificateRepo.DeleteById(
                    traineeId,
                    courseId
                );

                if (courseCertificate == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("courseCertificate")
                    );
                }

                return Ok(
                    Utils.CommonResponse.DeleteObjectSuccessfully(
                        "courseCertificate",
                        courseCertificate
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

        [HttpPut("{traineeId}/{courseId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCourseCertificate(
            int traineeId,
            int courseId,
            UpdateCourseCertificateDto updateCourseCertificateDto
        )
        {
            try
            {
                var courseCertificate = _courseCertificateRepo.Update(
                    traineeId,
                    courseId,
                    updateCourseCertificateDto
                );

                if (courseCertificate == null)
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("courseCertificate")
                    );
                }

                return Ok(
                    Utils.CommonResponse.UpdateObjectSuccessfully(
                        "courseCertificate",
                        courseCertificate
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
