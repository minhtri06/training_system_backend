namespace backend.Models
{
    public class DepartmentLearningPath
    {
        public int DepartmentId { get; set; }
        public int LearningPathId { get; set; }

        public Department? Department { get; set; }
        public LearningPath? LearningPath { get; set; }
    }
}
