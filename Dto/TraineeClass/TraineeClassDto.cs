using backend.Models;

namespace backend.Dto.TraineeClass
{
    public class TraineeClassDto
    {
        public int TraineeId { get; set; }
        public int ClassId { get; set; }
        public float? GPA { get; set; }
        public TraineeLearningState Status { get; set; }
        public int? CourseId { get; set; }
    }
}
