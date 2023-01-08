namespace backend.Models
{
    public class DepartmentLearningPath
    {
        public int DepartmentId { get; set; }
        public int LearningPathId { get; set; }

        public Department Department { get; set; } = null!;
        public LearningPath LearningPath { get; set; } = null!;
    }
}
