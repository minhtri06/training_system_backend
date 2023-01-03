using Microsoft.AspNetCore.Mvc;

using backend.Services.Interfaces;
using backend.Dto.Role;
using backend.Dto.ApiResponse;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : Controller { }
}
