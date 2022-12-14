using backend.Models;

namespace backend.Dto.AdminUser
{
    public class AdminUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public SystemRole SystemRole { get; set; }
        public int? RefreshTokenId { get; set; }
    }
}
