namespace backend.Dto.Class
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CourseId { get; set; }
    }
}