namespace backend.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Level { get; set; } = null!;
        public SystemRole SystemRole { get; set; }
        public string ImgLink { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public int? RoleId { get; set; }
        public int? DepartmentId { get; set; }
        public int? RefreshTokenId { get; set; }

        public Role? Role { get; set; }
        public Department? Department { get; set; }
        public RefreshToken? RefreshToken { get; set; }
        public ICollection<TraineeClass> TraineeClasses { get; set; }
        public ICollection<CourseCertificate> CourseCertificates { get; set; }
        public ICollection<LearningPathCertificate> LearningPathCertificates { get; set; }

        public Trainee()
        {
            TraineeClasses = new List<TraineeClass>();
            CourseCertificates = new List<CourseCertificate>();
            LearningPathCertificates = new List<LearningPathCertificate>();
        }
    }
}
