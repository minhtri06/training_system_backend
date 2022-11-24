namespace backend.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImgLink { get; set; }
        public SystemRole SystemRole { get; set; }
        public string? passwordHash { get; set; }
        public string? PasswordSalt { get; set; }

        public ICollection<Course> Courses { get; set; }

        public Trainer()
        {
            Courses = new List<Course>();
        }
    }
}
