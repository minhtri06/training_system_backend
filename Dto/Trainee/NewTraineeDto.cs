using backend.Models;

namespace backend.Dto.Trainee
{
    public class NewTraineeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Level { get; set; }
        public SystemRole? SystemRole { get; set; }
        public string? ImgLink { get; set; }
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;

        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
    }
}
