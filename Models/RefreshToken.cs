namespace backend.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = "";
        public DateTime ExpiryTime { get; set; }
        public DateTime CreatedTime { get; set; }

        public Trainee? Trainee { get; set; }
        public Trainer? Trainer { get; set; }
        public AdminUser? AdminUser { get; set; }
    }
}
