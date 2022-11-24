namespace backend.Models
{
    public class CourseCertificate
    {
        public int TraineeId { get; set; }
        public int CourseId { get; set; }

        public Trainee? Trainee { get; set; }
        public Course? Course { get; set; }
    }
}
