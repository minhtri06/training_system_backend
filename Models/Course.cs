namespace backend.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Online { get; set; }
        public int Duration { get; set; }
        public string LearningObjective { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? TrainerId { get; set; }

        public Trainer? Trainer { get; set; }
        public ICollection<LearningPathCourse> LearningPathCourses { get; set; }
        public ICollection<Class> Classes { get; set; }
        public ICollection<CourseCertificate> CourseCertificates { get; set; }

        public Course()
        {
            Classes = new List<Class>();
            LearningPathCourses = new List<LearningPathCourse>();
            CourseCertificates = new List<CourseCertificate>();
        }
    }
}
