namespace backend.Models
{
    public class LearningPathCourse
    {
        public int LearningPathId { get; set; }
        public int CourseId { get; set; }
        public int CourseOrder { get; set; }

        public LearningPath LearningPath { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
