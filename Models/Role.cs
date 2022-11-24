namespace backend.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Trainee> Trainees { get; set; }
        public ICollection<LearningPath> LearningPaths { get; set; }

        public Role()
        {
            Trainees = new List<Trainee>();
            LearningPaths = new List<LearningPath>();
        }
    }
}
