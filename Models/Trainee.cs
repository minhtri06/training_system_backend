namespace backend.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Level { get; set; }
        public SystemRole? SystemRole { get; set; }
        public string? ImgLink { get; set; }
        public string? username { get; set; }
        public string? passwordHash { get; set; }
        public string? PasswordSalt { get; set; }

        public Role? Role { get; set; }
        public Department? Department { get; set; }
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
