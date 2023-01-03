namespace backend.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? PasswordSalt { get; set; }
        public string? PasswordHash { get; set; }
        public string? ImgLink { get; set; }
        public SystemRole SystemRole { get; set; }
        public int? TokenId { get; set; }

        public RefreshToken? RefreshToken { get; set; }
    }
}
