namespace backend.Dto.Token
{
    public class RefreshTokenDto
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiryTime { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
