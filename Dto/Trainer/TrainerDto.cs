using backend.Models;

namespace backend.Dto.Trainer
{
    public class TrainerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public SystemRole SystemRole { get; set; }
        public string Username { get; set; } = null!;
        public int? RefreshTokenId { get; set; }
    }
}
