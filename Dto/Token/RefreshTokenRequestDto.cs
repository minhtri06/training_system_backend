namespace backend.Dto.Token
{
    public class RefreshTokenRequestDto
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
