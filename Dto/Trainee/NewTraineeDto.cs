using backend.Models;

namespace backend.Dto.Trainee
{
    public class NewTraineeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Level { get; set; }
        public string? ImgLink { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public int? RoleId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
