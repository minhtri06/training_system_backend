using backend.Models;

namespace backend.Dto.Trainee
{
    public class NewTraineeDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Level { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public int? RoleId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
