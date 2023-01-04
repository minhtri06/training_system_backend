namespace backend.Dto.LearningPath
{
    public class LearningPathDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public int? ForRoleId { get; set; }
    }
}
