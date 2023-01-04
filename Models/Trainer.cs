namespace backend.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ImgLink { get; set; }
        public SystemRole SystemRole { get; set; }
        public string Username { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public int? RefreshTokenId { get; set; }

        public RefreshToken? RefreshToken { get; set; }
        public ICollection<Course> Courses { get; set; }

        public Trainer()
        {
            Courses = new List<Course>();
        }
    }
}
