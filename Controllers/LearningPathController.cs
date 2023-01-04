using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;

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
    }
}
