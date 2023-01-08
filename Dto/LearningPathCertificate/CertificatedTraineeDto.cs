using backend.Models;

namespace backend.Dto.Trainee
{
    public class CertificatedTraineeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Level { get; set; } = null!;
        public string ImgLink { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
    }
}
