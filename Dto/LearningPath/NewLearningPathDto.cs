namespace backend.Dto.LearningPath
{
    public class NewLearningPathDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public int? ForRoleId { get; set; }
    }
}
