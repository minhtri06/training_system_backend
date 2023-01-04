namespace backend.Dto.Course
{
    public class NewCourseDto
    {        
        public string Name { get; set;  } = null!;
        public bool Online { get; set; }
        public int Duration { get; set; }
        public string LearningObjective { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? TrainerId { get; set; }
    }
}