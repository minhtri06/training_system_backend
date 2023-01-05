namespace backend.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<DepartmentLearningPath> DepartmentLearningPaths { get; set; }
        public ICollection<Trainee> Trainees { get; set; }

        public Department()
        {
            DepartmentLearningPaths = new List<DepartmentLearningPath>();
            Trainees = new List<Trainee>();
        }
    }
}
