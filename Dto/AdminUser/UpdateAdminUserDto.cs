using backend.Models;

namespace backend.Dto.AdminUser
{
    public class UpdateAdminUserDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
    }
}
