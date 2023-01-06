namespace backend.Dto.LearningPathCertificate
{
    public class NewLearningPathCertificateDto
    {
        public int TraineeId { get; set; }
        public int LearningPathId { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
    }
}
