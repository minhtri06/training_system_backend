using Microsoft.AspNetCore.Mvc;

using backend.Dto.ApiResponse;
using backend.Services.Interfaces;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeController : Controller
    {
        private readonly ITraineeRepository _traineeRepo;

        public TraineeController(ITraineeRepository traineeRepo)
        {
            _traineeRepo = traineeRepo;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var trainees = _traineeRepo.GetAllTrainees();
            return Ok(new ApiResponseDto() {
                Success = true,
                Message = "Get all trainees successfull",
                Data = trainees
            });
        }
    }
}
