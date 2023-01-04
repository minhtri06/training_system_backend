namespace backend.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public SystemRole SystemRole { get; set; }
        public int? RefreshTokenId { get; set; }

        public RefreshToken? RefreshToken { get; set; }
    }
}
