namespace backend.Models
{
    public class LearningPathCertificate
    {
        public int TraineeId { get; set; }
        public int LearningPathId { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        public virtual Trainee Trainee { get; set; } = null!;
        public virtual LearningPath LearningPath { get; set; } = null!;
    }
}
