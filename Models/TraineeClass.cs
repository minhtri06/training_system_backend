namespace backend.Models
{
    public class TraineeClass
    {
        public int TraineeId { get; set; }
        public int ClassId { get; set; }
        public float GPA { get; set; }
        public TraineeLearningState Status { get; set; }
        public int CourseId { get; set; }

        public Trainee? Trainee { get; set; }
        public Class? Class { get; set; }
    }
}
