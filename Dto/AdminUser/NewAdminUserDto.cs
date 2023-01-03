using backend.Models;

namespace backend.Dto.AdminUser
{
    public class NewAdminUserDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
    }
}
