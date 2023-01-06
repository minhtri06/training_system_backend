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

        public LearningPathController(
            ILearningPathRepository learningPathRepo,
            IRoleRepository roleRepo
        )
        {
            _learningPathRepo = learningPathRepo;
            _roleRepo = roleRepo;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllLearningPath()
        {
            var learningPaths = _learningPathRepo.GetAll();

            return Ok(
                Utils.CommonResponse.GetAllObjectsSuccessfully(
                    "learning paths",
                    learningPaths
                )
            );
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateLearningPath(
            NewLearningPathDto newLearningPathDto
        )
        {
            if (newLearningPathDto.ForRoleId != null)
            {
                if (
                    _roleRepo.CheckIdExist((int)newLearningPathDto.ForRoleId)
                    == false
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

        [HttpGet("{learningPathId}")]
        [Authorize]
        public IActionResult GetLearningPathById(int learningPathId)
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

        [HttpDelete("{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteLearningPathById(int learningPathId)
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

        [HttpPut("{learningPathId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateLearningPath(
            int learningPathId,
            UpdateLearningPathDto updateLearningPathDto
        )
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
                    _roleRepo.CheckIdExist((int)updateLearningPathDto.ForRoleId)
                    == false
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
    }
}
