namespace backend.Models
{
    public class LearningPath
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public int? ForRoleId { get; set; }

        public Role? ForRole { get; set; }
        public ICollection<DepartmentLearningPath> DepartmentLearningPaths { get; set; }
        public ICollection<LearningPathCourse> LearningPathCourses { get; set; }
        public ICollection<LearningPathCertificate> LearningPathCertificates { get; set; }

        public LearningPath()
        {
            DepartmentLearningPaths = new List<DepartmentLearningPath>();
            LearningPathCourses = new List<LearningPathCourse>();
            LearningPathCertificates = new List<LearningPathCertificate>();
        }
    }
}
