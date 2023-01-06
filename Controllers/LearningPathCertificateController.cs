using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using backend.Services.Interfaces;
using backend.Dto.LearningPathCertificate;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningPathCertificateController : Controller
    {
        private readonly ILearningPathCertificateRepository _learningPathCertificateRepo;
        private readonly ITraineeRepository _traineeRepo;
        private readonly ILearningPathRepository _learningPathRepo;

        public LearningPathCertificateController(
            ILearningPathCertificateRepository learningPathCertificateRepo,
            ITraineeRepository traineeRepo,
            ILearningPathRepository learningPathRepo
        )
        {
            _learningPathCertificateRepo = learningPathCertificateRepo;
            _traineeRepo = traineeRepo;
            _learningPathRepo = learningPathRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllLearningPathCertificate()
        {
            var learningPathCertificates = _learningPathCertificateRepo.GetAll();

            return Ok(
                Utils.CommonResponse.GetAllObjectsSuccessfully(
                    "learning path certificates",
                    learningPathCertificates
                )
            );
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateLearningPathCertificate(
            NewLearningPathCertificateDto newLearningPathCertificateDto
        )
        {
            try
            {
                if (
                    _traineeRepo.CheckIdExist(
                        newLearningPathCertificateDto.TraineeId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("trainee")
                    );
                }

                if (
                    _learningPathRepo.CheckIdExist(
                        newLearningPathCertificateDto.LearningPathId
                    ) == false
                )
                {
                    return NotFound(
                        Utils.CommonResponse.ObjectNotFound("learningPath")
                    );
                }

                if (
                    _learningPathCertificateRepo.CheckIdExist(
                        newLearningPathCertificateDto.TraineeId,
                        newLearningPathCertificateDto.LearningPathId
                    )
                )
                {
                    return BadRequest(
                        Utils.CommonResponse.ObjectAlreadyExist(
                            "learningpath certificate"
                        )
                    );
                }

                var newLearningPathCertificate =
                    _learningPathCertificateRepo.Create(
                        newLearningPathCertificateDto
                    );

                return Ok(
                    Utils.CommonResponse.CreateObjectSuccessfully(
                        "learningpath certificate",
                        newLearningPathCertificate
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

        [HttpGet("{traineeId}/{learningPathId}")]
        [Authorize]
        public IActionResult GetLearningPathCertificateById(int traineeId, int learningPathId)
        {
            if (_learningPathCertificateRepo.CheckIdExist(traineeId, learningPathId) == false)
            {
                return NotFound(
                    Utils.CommonResponse.ObjectNotFound("learning path certificate")
                );
            }

            var learningPathCertificate = _learningPathCertificateRepo.GetById(traineeId, learningPathId);

            return Ok(
                Utils.CommonResponse.GetObjectSuccessfully(
                    "learning path certificate",
                    learningPathCertificate
                )
            );
        }

        [HttpDelete("{traineeId}/{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteLearningPathCertificateById(int traineeId, int learningPathId)
        {
            if (_learningPathCertificateRepo.CheckIdExist(traineeId, learningPathId) == false)
            {
                return NotFound(
                    Utils.CommonResponse.ObjectNotFound("learning path certificate")
                );
            }

            var deletedLearningPathCertificate = _learningPathCertificateRepo.DeleteById(
                traineeId, learningPathId
            );

            return Ok(
                Utils.CommonResponse.DeleteObjectSuccessfully(
                    "learning path",
                    deletedLearningPathCertificate
                )
            );
        }

        [HttpPut("{traineeId}/{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateLearningPathCertificate(
            int traineeId, int learningPathId,
            UpdateLearningPathCertificateDto updateLearningPathCertificateDto
        )
        {
            if (_learningPathCertificateRepo.CheckIdExist(traineeId, learningPathId) == false)
            {
                return NotFound(
                    Utils.CommonResponse.ObjectNotFound("learning path certificate id")
                );
            }

            var updatedLearningPathCertificate = _learningPathCertificateRepo.Update(
                traineeId, learningPathId,
                updateLearningPathCertificateDto
            );

            return Ok(
                Utils.CommonResponse.UpdateObjectSuccessfully(
                    "learning path certificate",
                    updatedLearningPathCertificate
                )
            );
        }
    }
}
