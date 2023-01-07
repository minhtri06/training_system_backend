using backend.Models;

namespace backend.Dto.Trainer
{
    public class NewTrainerDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
