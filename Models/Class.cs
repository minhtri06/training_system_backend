namespace backend.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Course? Course { get; set; }
        public ICollection<TraineeClass> TraineeClasses { get; set; }

        public Class()
        {
            TraineeClasses = new List<TraineeClass>();
        }
    }
}
