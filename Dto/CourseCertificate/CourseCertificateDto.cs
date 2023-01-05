namespace backend.Dto.CourseCertificate
{
    public class CourseCertificateDto
    {
        public int TraineeId { get; set; }
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
    }
}
