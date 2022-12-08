using backend.Models;

namespace backend.Dto.Trainee
{
    public class TraineeDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Level { get; set; }
        public SystemRole? SystemRole { get; set; }
        public string? ImgLink { get; set; }
        public string? username { get; set; }

        public int? RoleId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
