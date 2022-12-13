namespace backend.Models
{
    public class CourseCertificate
    {
        public int TraineeId { get; set; }
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        public Trainee? Trainee { get; set; }
        public Course? Course { get; set; }
    }
}
