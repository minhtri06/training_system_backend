namespace backend.Dto.CourseCertificate
{
    public class NewCourseCertificateDto
    {
        public int TraineeId { get; set; }
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
    }
}
