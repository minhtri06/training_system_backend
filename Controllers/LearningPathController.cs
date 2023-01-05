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

        public LearningPathController(ILearningPathRepository learningPathRepo)
        {
            _learningPathRepo = learningPathRepo;
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

        // [HttpPost]
        // [Authorize(Roles = "Admin")]
        // public IActionResult CreateLearningPath(
        //     NewLearningPathDto newLearningPathDto
        // ) { }
    }
}
