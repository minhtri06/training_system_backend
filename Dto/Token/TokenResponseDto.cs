using backend.Dto.AdminUser;

namespace backend.Dto.Token
{
    public class TokenResponseDto
    {
        public Object userInfo { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
