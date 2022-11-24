namespace backend.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<LearningPath> LearningPaths { get; set; }
        public ICollection<Trainee> Trainees { get; set; }

        public Department()
        {
            LearningPaths = new List<LearningPath>();
            Trainees = new List<Trainee>();
        }
    }
}
